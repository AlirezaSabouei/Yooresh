using Yooresh.Blazor.Village.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Yooresh.Blazor.Village.Services;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _httpClient;

    public PlayerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreatePlayer(CreatePlayerDto createPlayerDto)
    {
        StringContent content = new(JsonConvert.SerializeObject(createPlayerDto), Encoding.UTF8, "application/json");
        HttpResponseMessage result = await _httpClient.PostAsync("players", content);
        return await result.Content.ReadFromJsonAsync<bool>();
    }
}
