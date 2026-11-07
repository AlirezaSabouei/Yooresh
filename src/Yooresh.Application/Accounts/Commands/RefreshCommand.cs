using MediatR;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Commands;

public class RefreshCommand : IRequest<Dictionary<string, string>?>
{
    public string RefreshToken { get; set; } = null!;
}

public class RefreshCommandHandler(
    IDocumentStore<Player> documentStore, ITokenService tokenService, IDateTimeProvider dateTimeProvider)
    : IRequestHandler<RefreshCommand, Dictionary<string, string>?>
{
    private readonly IDocumentStore<Player> _documentStore = documentStore;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Dictionary<string, string>?> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var players = await _documentStore.GetAsync(
            a => a.RefreshTokens.Any(t => t.Token == request.RefreshToken), cancellationToken);

        var player = players.FirstOrDefault();
        var storedToken = player?.RefreshTokens
            .FirstOrDefault(a => a.Token == request.RefreshToken);

        if (player is null || storedToken is null)
        {
            return null;
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
            player.Id.ToString(),
            player.Role.ToString(),
            player.Email,
            player.Name);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        storedToken.IsRevoked = true;
        player.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            Expires = _dateTimeProvider.GetUtcNow().AddDays(10),
            IsRevoked = false,
            Token = newRefreshToken
        });
        await _documentStore.UpdateAsync(player, cancellationToken);

        var result = new Dictionary<string, string>
        {
            { "token", newAccessToken },
            { "refreshToken", newRefreshToken }
        };
        return result;
    }
}
