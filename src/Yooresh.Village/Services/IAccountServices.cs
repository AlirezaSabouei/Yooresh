using Yooresh.Village.Models;
using Yooresh.Village.Models.Accounts;

namespace Yooresh.Village.Services;

public interface IAccountServices
{
    public Task<Result<Player>> SignUp(SignUpDto signUpDto);
    public Task<Result<bool>> ConfirmAccount(ConfirmAccountDto confirmAccountDto);
    Task<Result<Dictionary<string, string>>> Login(LoginDto loginDto);
}
