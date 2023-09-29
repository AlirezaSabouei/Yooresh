using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.Application.Villages.Queries;

public record GetAvailableResourceBuildingUpgradesQuery : IRequest<List<ResourceBuilding>>
{
    public Guid PlayerId { get; set; }
}

public class GetAvailableBuildingUpgradesQueryHandler
    : IRequestHandler<GetAvailableResourceBuildingUpgradesQuery, List<ResourceBuilding>>
{
    private readonly IContext _context;

    public GetAvailableBuildingUpgradesQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<List<ResourceBuilding>> Handle(GetAvailableResourceBuildingUpgradesQuery request,
        CancellationToken cancellationToken)
    {
        var village = await _context
            .Villages
            //.Include(a=>a.ResourceBuildings)
            //.ThenInclude(a=>a.ResourceBuilding)
            //.ThenInclude(a=>a.Requirement)

           // .Include(a=>a.Upgrades)
            .FirstAsync(a => a.PlayerId == request.PlayerId,cancellationToken);
        
        village.RefreshVillage();
        
        //Union other buildings later
        var villageResourceBuildings=village.ResourceBuildings
            .Select(a=>a.ResourceBuilding)
            .ToList();

        var villageUpgrades=village.Upgrades.Select(a=>a.ToId).ToList();

        var level1Buildings = await _context.ResourceBuildings
                .Where(a => a.Requirement == null && !villageResourceBuildings.Contains(a) && !villageUpgrades.Contains(a.Id))
                .ToListAsync(cancellationToken);

        var upgradeableBuildings = await _context.ResourceBuildings
            .Where(a => villageResourceBuildings.Select(b=>b.Requirement).Contains(a.Requirement) && !villageUpgrades.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var readyToUpdateBuildings = level1Buildings
            .Union(upgradeableBuildings)
            .ToList();

        return readyToUpdateBuildings;
    }
}