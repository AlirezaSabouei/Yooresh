using Yooresh.Blazor.Village.Models.Players;

namespace Yooresh.Blazor.Village.Services;

public interface IPlayerServices
{
    public Task<Player> SignUp(SignUpDto signUpDto);
}
