using Yooresh.Blazor.Village.Models.Players;

namespace Yooresh.Blazor.Village.Services;

public class PlayerServices(HttpClient httpClient) : IPlayerServices
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Models.Resource>?> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<List<Models.Resource>>("api/resources");
    }

    public async Task<Player> SignUp(SignUpDto signUpDto)
    {
        var result = await _httpClient.PostAsJsonAsync("api/players/signup", signUpDto);
        return await result.Content.ReadFromJsonAsync<Player>();
    }
}
