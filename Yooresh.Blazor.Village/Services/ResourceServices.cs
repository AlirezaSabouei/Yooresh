namespace Yooresh.Blazor.Village.Services;

public class ResourceServices(HttpClient httpClient) : IResourceServices
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Models.Resource>?> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<List<Models.Resource>>("api/resources");
    }
}
