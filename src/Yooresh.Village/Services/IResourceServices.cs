using Yooresh.Village.Models.Resources;

namespace Yooresh.Village.Services;

public interface IResourceServices
{
    public Task<List<ResourceModel>?> GetResources();
}
