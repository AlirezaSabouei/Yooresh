using Yooresh.Village.Models;
using Yooresh.Village.Models.ResourceBuildings;

namespace Yooresh.Village.Services;

public interface IResourceBuildingsServices
{
    Task<Result<List<ResourceBuildingModel>>> GetResourceBuildings();
}
