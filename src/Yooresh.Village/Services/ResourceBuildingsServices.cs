using System.Net.Http.Headers;
using Yooresh.Village.Models;
using Yooresh.Village.Models.ResourceBuildings;

namespace Yooresh.Village.Services;

public class ResourceBuildingsServices(HttpClient httpClient, AuthenticationService authenticationService)
    : IResourceBuildingsServices
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AuthenticationService _authenticationService = authenticationService;

    public async Task<Result<List<ResourceBuildingModel>>> GetResourceBuildings()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Token);
            var response = await _httpClient.GetAsync("api/resourceBuildings");
            if (response.IsSuccessStatusCode)
            {
                var resourceBuildings = await response.Content.ReadFromJsonAsync<List<ResourceBuildingModel>>();
                return Result<List<ResourceBuildingModel>>.Success(resourceBuildings!);
            }
            return Result<List<ResourceBuildingModel>>.Fail("Getting resource buildings failed: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return Result<List<ResourceBuildingModel>>.Fail("Getting resource buildings failed: " + ex.Message);
        }
    }
}
