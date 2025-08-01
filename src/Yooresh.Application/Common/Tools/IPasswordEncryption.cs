using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Common.Tools;

public interface IPasswordEncryption
{
    string HashPassword(Player player);
}
