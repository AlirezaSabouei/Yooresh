using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Factions;

namespace Yooresh.Application.Factions.Queries;

public record GetFactionsQuery : IRequest<List<Faction>>
{
}

public class GetFactionsQueryHandler : IRequestHandler<GetFactionsQuery, List<Faction>>
{
    private const string CacheKey = "Factions";

    private readonly IMemoryCache _memoryCache;
    private readonly IDocumentStore<Faction> _documentStore;

    public GetFactionsQueryHandler(IMemoryCache memoryCache, IDocumentStore<Faction> documentStore)
    {
        _memoryCache = memoryCache;
        _documentStore = documentStore;
    }

    public async Task<List<Faction>> Handle(GetFactionsQuery request, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(CacheKey, out List<Faction>? factions) && factions is not null)
        {
            return factions;
        }

        factions = await _documentStore.GetAsync(_ => true, cancellationToken);
        _memoryCache.Set(CacheKey, factions);
        return factions;
    }
}
