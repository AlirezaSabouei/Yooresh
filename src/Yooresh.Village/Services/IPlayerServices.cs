using Yooresh.Village.Models;
using Yooresh.Village.Models.Players;

namespace Yooresh.Village.Services;

public interface IPlayerServices
{
    public Task<Result<Player>> SignUp(SignUpDto signUpDto);
    public Task<Result<bool>> ConfirmAccount(ConfirmAccountDto confirmAccountDto);
}
