using MediatR;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Commands;

public class LoginCommand : IRequest<Dictionary<string, string>?>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandHandler(
    IDocumentStore<Player> documentStore,
    ITokenService tokenService,
    IDateTimeProvider dateTimeProvider,
    IPasswordEncryption<BaseEntity> passwordEncryption)
    : IRequestHandler<LoginCommand, Dictionary<string, string>?>
{
    private readonly IDocumentStore<Player> _documentStore = documentStore;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IPasswordEncryption<BaseEntity> _passwordEncryption = passwordEncryption;

    public async Task<Dictionary<string, string>?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var confirmedPlayers = await _documentStore.GetAsync(a => a.Confirmed, cancellationToken);

        var player = confirmedPlayers.FirstOrDefault(a =>
            string.Equals(a.Email, request.Email, StringComparison.OrdinalIgnoreCase));

        if (player is null)
        {
            return null;
        }

        var success = _passwordEncryption.VerifyPassword(player, player.Password, request.Password);
        if (!success)
        {
            return null;
        }

        var accessToken = _tokenService.GenerateAccessToken(player.Id.ToString(), player.Role.ToString(), player.Email, player.Name);
        var refreshToken = _tokenService.GenerateRefreshToken();
        player.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            Expires = _dateTimeProvider.GetUtcNow().AddDays(10),
            IsRevoked = false,
            Token = refreshToken
        });
        await _documentStore.UpdateAsync(player, cancellationToken);

        var result = new Dictionary<string, string>
        {
            { "token", accessToken },
            { "refreshToken", refreshToken },
            { "playerId", player.Id.ToString() },
            { "name", player.Name },
            { "email", player.Email },
        };
        return result;
    }
}
