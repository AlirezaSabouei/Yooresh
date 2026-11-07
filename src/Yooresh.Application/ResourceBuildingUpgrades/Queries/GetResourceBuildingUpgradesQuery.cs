using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;

namespace Yooresh.Application.ResourceBuildingUpgrades.Queries;

public class GetResourceBuildingUpgradesQuery : IRequest<List<ResourceBuildingUpgrade>>
{
    public List<ResourceBuildingType> ResourceBuildingTypes { get; set; }
    public int Level { get; set; }

    public GetResourceBuildingUpgradesQuery()
    {
        ResourceBuildingTypes = [];
    }
}

public class GetResourceBuildingUpgradesQueryHandler(
    IDocumentStore<ResourceBuildingUpgrade> documentStore, IMemoryCache memoryCache) :
    IRequestHandler<GetResourceBuildingUpgradesQuery, List<ResourceBuildingUpgrade>>
{
    private readonly IDocumentStore<ResourceBuildingUpgrade> _documentStore = documentStore;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<List<ResourceBuildingUpgrade>> Handle(
        GetResourceBuildingUpgradesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = BuildCacheKey(request);

        if (_memoryCache.TryGetValue(cacheKey, out List<ResourceBuildingUpgrade>? resourceBuildingUpgrades)
            && resourceBuildingUpgrades is not null)
        {
            return resourceBuildingUpgrades;
        }

        var matchingLevel = await _documentStore.GetAsync(a => a.Level == request.Level, cancellationToken);
        resourceBuildingUpgrades = matchingLevel
            .Where(a => request.ResourceBuildingTypes.Contains(a.ResourceBuildingType))
            .ToList();

        _memoryCache.Set(cacheKey, resourceBuildingUpgrades);
        return resourceBuildingUpgrades;
    }

    private static string BuildCacheKey(GetResourceBuildingUpgradesQuery request)
    {
        var types = string.Join(',', request.ResourceBuildingTypes.OrderBy(a => a));
        return $"ResourceBuildingUpgrades:{request.Level}:{types}";
    }
}
