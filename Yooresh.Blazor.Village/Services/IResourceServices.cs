using Yooresh.Blazor.Village.Models;

namespace Yooresh.Blazor.Village.Services;

public interface IResourceServices
{
    public Task<List<Resource>?> GetResources();
}
