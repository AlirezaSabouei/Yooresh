using Users.Models;

namespace Users.Services;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user);
    RefreshToken GenerateRefreshToken();
}