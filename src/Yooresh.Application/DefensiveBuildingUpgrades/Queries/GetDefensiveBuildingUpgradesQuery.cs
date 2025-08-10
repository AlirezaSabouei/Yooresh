using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
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

public class GetDefensiveBuildingUpgradesQueryHandler(IContext context) :
    IRequestHandler<GetDefensiveBuildingUpgradesQuery, List<DefensiveBuildingUpgrade>>
{
    private readonly IContext _context = context;

    public async Task<List<DefensiveBuildingUpgrade>> Handle(
        GetDefensiveBuildingUpgradesQuery request, CancellationToken cancellationToken)
    {
        var defensiveBuildingUpgrades = await _context.DefensiveBuildingUpgrades
            .Where(a => a.Level == request.Level)
            .Where(a => request.DefensiveBuildingTypes.Contains(a.DefensiveBuildingType))
            .ToListAsync(cancellationToken);
        return defensiveBuildingUpgrades;
    }
}
