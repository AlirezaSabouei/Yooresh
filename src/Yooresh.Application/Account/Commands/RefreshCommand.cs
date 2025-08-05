using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Account.Commands;

public class RefreshCommand : IRequest<Dictionary<string, string>?>
{
    public string RefreshToken { get; set; } = null!;
}

public class RefreshCommandHandler(IContext context, ITokenService tokenService, IDateTimeProvider dateTimeProvider)
    : IRequestHandler<RefreshCommand, Dictionary<string, string>?>
{
    private readonly IContext _context = context;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Dictionary<string, string>?> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? storedToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(a => a.Token == request.RefreshToken, cancellationToken);

        if (storedToken is null)
        {
            return null;
        }
        var newAccessToken = _tokenService.GenerateAccessToken(
            storedToken.Player.Id.ToString(),
            storedToken.Player.Role.ToString(),
            storedToken.Player.Email,
            storedToken.Player.Name);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        storedToken.IsRevoked = true;
        storedToken.Player.RefreshTokens.Add(new RefreshToken()
        {
            Id = Guid.NewGuid(),
            Expires = _dateTimeProvider.GetUtcNow().AddDays(10),
            IsRevoked = false,
            Token = newRefreshToken
        });
        await _context.SaveChangesAsync(cancellationToken);

        var result = new Dictionary<string, string>
        {
            { "token", newAccessToken },
            { "refreshToken", newRefreshToken }
        };
        return result;
    }
}