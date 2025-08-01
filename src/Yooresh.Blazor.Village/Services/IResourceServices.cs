using Yooresh.Village.Models;

namespace Yooresh.Village.Services;

public interface IResourceServices
{
    public Task<List<Resource>?> GetResources();
}
