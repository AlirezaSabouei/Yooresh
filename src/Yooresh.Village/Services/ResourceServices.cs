using System.Net.Http.Headers;
using Yooresh.Village.Models.Resources;

namespace Yooresh.Village.Services;

public class ResourceServices(HttpClient httpClient, AuthenticationService authenticationService) : IResourceServices
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AuthenticationService _authenticationService = authenticationService;

    public async Task<List<ResourceModel>?> GetResources()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Token);
        return await _httpClient.GetFromJsonAsync<List<ResourceModel>>("api/resources");
    }
}
