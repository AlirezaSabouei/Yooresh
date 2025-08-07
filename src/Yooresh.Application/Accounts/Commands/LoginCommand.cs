using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;

namespace Yooresh.Application.Accounts.Commands;

public class LoginCommand : IRequest<Dictionary<string, string>?>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandHandler(
    IContext context,
    ITokenService tokenService,
    IDateTimeProvider dateTimeProvider,
    IPasswordEncryption<BaseEntity> passwordEncryption)
    : IRequestHandler<LoginCommand, Dictionary<string, string>?>
{
    private readonly IContext _context = context;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IPasswordEncryption<BaseEntity> _passwordEncryption = passwordEncryption;

    public async Task<Dictionary<string, string>?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players
            .Where(a => a.Confirmed)
            .FirstOrDefaultAsync(a => a.Email.ToLower() == request.Email.ToLower(), cancellationToken);

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
        _context.RefreshTokens.Add(new Domain.Entities.Players.RefreshToken()
        {
            Id = Guid.NewGuid(),
            Expires = _dateTimeProvider.GetUtcNow().AddDays(10),
            IsRevoked = false,
            Token = refreshToken,
            Player = player
        });
        await _context.SaveChangesAsync(cancellationToken);

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