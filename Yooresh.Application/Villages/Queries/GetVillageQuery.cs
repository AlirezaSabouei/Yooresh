using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Villages;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var village = await _context.Villages
            //.Include(a=>a.Faction)
           // .Include(a=>a.Player)
            .Include(a => a.Upgrades)
            .Include(a=>a.ResourceBuildings).ThenInclude(b=>b.ResourceBuilding)
            .FirstAsync(a => a.PlayerId == request.PlayerId, cancellationToken);
        //  village.RefreshVillage();

        village.ResourceBuildings.Add(new VillageResourceBuilding(village.Id, new Guid("769A5118-F48B-4E55-B5FD-616D22A357B0")));
        var test = _context.Test(village);
        await _context.SaveChangesAsync(cancellationToken);
        
        return village;
    }
}