using Yooresh.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.Application.Villages.Queries;

public record GetVillageQuery : IRequest<Village>
{
    public Guid PlayerId { get; set; }
}

public class GetVillageQueryHandler : IRequestHandler<GetVillageQuery, Village>
{
    private readonly IContext _context;

    public GetVillageQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Village> Handle(GetVillageQuery request, CancellationToken cancellationToken)
    { 
        var village = (await _context.Villages
            .Include(a => a.VillageResourceBuildings)
            .ThenInclude(b => b.Building)
            .ThenInclude(c => c.Target)
            .Include(a => a.VillageTower)
            .ThenInclude(b => b.Tower)
            .ThenInclude(c => c.Target)
            .Include(a => a.VillageWall)
            .ThenInclude(b => b.Wall)
            .ThenInclude(c => c.Target)
            .FirstAsync(a => a.PlayerId == request.PlayerId, cancellationToken))
            .GatherResources();

        await _context.SaveChangesAsync(cancellationToken);

        return village;
    }
}