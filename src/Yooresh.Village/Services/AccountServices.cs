using System.Net.Http.Headers;
using Yooresh.Village.Models;
using Yooresh.Village.Models.Account;

namespace Yooresh.Village.Services;

public class AccountServices(HttpClient httpClient, AuthenticationService authenticationService) : IAccountServices
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AuthenticationService _authenticationService = authenticationService;

    public async Task<Result<Player>> SignUp(SignUpDto signUpDto)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync("api/players/signup", signUpDto);
            if (result.IsSuccessStatusCode)
            {
                var player = await result.Content.ReadFromJsonAsync<Player>();
                return Result<Player>.Success(player!);
            }
            return Result<Player>.Fail("Sign up failed: " + await result.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return Result<Player>.Fail("Sign up failed: " + ex.Message);
        }
    }

    public async Task<Result<bool>> ConfirmAccount(ConfirmAccountDto confirmAccountDto)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync("api/players/confirm", confirmAccountDto);
            if (result.IsSuccessStatusCode)
            {
                var success = await result.Content.ReadFromJsonAsync<bool>();
                return Result<bool>.Success(success!);
            }
            return Result<bool>.Fail("Sign up failed: " + await result.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail("Sign up failed: " + ex.Message);
        }
    }

    public async Task<Result<Dictionary<string, string>>> Login(LoginDto loginDto)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync("api/players/login", loginDto);
            if (result.IsSuccessStatusCode)
            {
                var success = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", success["token"]);
                _authenticationService.Token = success["token"];
                _authenticationService.PlayerId = success["playerId"];
                _authenticationService.Name = success["name"];
                _authenticationService.Email = success["email"];
                return Result<Dictionary<string, string>>.Success(success!);
            }
            return Result<Dictionary<string, string>>.Fail("Login failed: " + await result.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return Result<Dictionary<string, string>>.Fail("Login failed: " + ex.Message);
        }
    }
}
