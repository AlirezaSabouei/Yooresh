using Yooresh.Village.Models;
using Yooresh.Village.Models.Players;

namespace Yooresh.Village.Services;

public class PlayerServices(HttpClient httpClient) : IPlayerServices
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Resource>?> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<List<Resource>>("api/resources");
    }

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
}
