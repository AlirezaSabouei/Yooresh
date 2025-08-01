using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Factions;

namespace Yooresh.Application.Factions.Queries;

public record GetFactionsQuery : IRequest<List<Faction>>
{
}

public class GetFactionsQueryHandler : IRequestHandler<GetFactionsQuery, List<Faction>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IContext _context;

    public GetFactionsQueryHandler(IMemoryCache memoryCache, IContext context)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public async Task<List<Faction>> Handle(GetFactionsQuery request, CancellationToken cancellationToken)
    {
        var list = new List<Faction>();

        if (!_memoryCache.TryGetValue(nameof(IContext.Factions), out list))
        {
            _memoryCache.Set(
                nameof(IContext.Factions),
                await _context.Factions
                    .AsNoTracking()
                    .ToListAsync(cancellationToken));
            _memoryCache.TryGetValue(nameof(IContext.Factions), out list);
        }

        return list!;
    }
}