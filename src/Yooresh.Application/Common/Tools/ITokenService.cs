namespace Yooresh.Application.Common.Tools
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string role);
        string GenerateRefreshToken();
    }
}