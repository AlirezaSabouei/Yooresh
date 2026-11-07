using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;

namespace Yooresh.Application.DefensiveBuildingUpgrades.Queries;

public class GetDefensiveBuildingUpgradesQuery : IRequest<List<DefensiveBuildingUpgrade>>
{
    public List<DefensiveBuildingType> DefensiveBuildingTypes { get; set; }
    public int Level { get; set; }

    public GetDefensiveBuildingUpgradesQuery()
    {
        DefensiveBuildingTypes = [];
    }
}

public class GetDefensiveBuildingUpgradesQueryHandler(
    IDocumentStore<DefensiveBuildingUpgrade> documentStore, IMemoryCache cache) :
    IRequestHandler<GetDefensiveBuildingUpgradesQuery, List<DefensiveBuildingUpgrade>>
{
    private readonly IDocumentStore<DefensiveBuildingUpgrade> _documentStore = documentStore;
    private readonly IMemoryCache _cache = cache;

    public async Task<List<DefensiveBuildingUpgrade>> Handle(
        GetDefensiveBuildingUpgradesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = BuildCacheKey(request);

        if (_cache.TryGetValue(cacheKey, out List<DefensiveBuildingUpgrade>? defensiveBuildingUpgrades)
            && defensiveBuildingUpgrades is not null)
        {
            return defensiveBuildingUpgrades;
        }

        var matchingLevel = await _documentStore.GetAsync(a => a.Level == request.Level, cancellationToken);
        defensiveBuildingUpgrades = matchingLevel
            .Where(a => request.DefensiveBuildingTypes.Contains(a.DefensiveBuildingType))
            .ToList();

        _cache.Set(cacheKey, defensiveBuildingUpgrades);
        return defensiveBuildingUpgrades;
    }

    private static string BuildCacheKey(GetDefensiveBuildingUpgradesQuery request)
    {
        var types = string.Join(',', request.DefensiveBuildingTypes.OrderBy(a => a));
        return $"DefensiveBuildingUpgrades:{request.Level}:{types}";
    }
}
