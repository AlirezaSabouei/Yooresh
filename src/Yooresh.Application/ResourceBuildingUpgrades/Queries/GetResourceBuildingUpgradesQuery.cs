using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
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

public class GetResourceBuildingUpgradesQueryHandler(IContext context) :
    IRequestHandler<GetResourceBuildingUpgradesQuery, List<ResourceBuildingUpgrade>>
{
    private readonly IContext _context = context;

    public async Task<List<ResourceBuildingUpgrade>> Handle(
        GetResourceBuildingUpgradesQuery request, CancellationToken cancellationToken)
    {
        var resourceBuildingUpgrades = await _context.ResourceBuildingUpgrades
            .Where(a => a.Level == request.Level)
            .Where(a => request.ResourceBuildingTypes.Contains(a.ResourceBuildingType))
            .ToListAsync(cancellationToken);
        return resourceBuildingUpgrades;
    }
}
