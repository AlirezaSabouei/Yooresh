using Microsoft.AspNetCore.Identity;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Infrastructure.PasswordTools;

public class PasswordEncryption : IPasswordEncryption
{
    public string HashPassword(Player player)
    {
        var hasher = new PasswordHasher<Player>();
        return hasher.HashPassword(player, player.Password);
    }
}
