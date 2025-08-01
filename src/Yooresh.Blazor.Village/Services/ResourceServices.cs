using Yooresh.Village.Models;

namespace Yooresh.Village.Services;

public class ResourceServices(HttpClient httpClient) : IResourceServices
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Resource>?> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<List<Resource>>("api/resources");
    }
}
