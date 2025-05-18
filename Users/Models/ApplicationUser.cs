using Microsoft.AspNetCore.Identity;

namespace Users.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}