using MediatR;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;
using Yooresh.Application.DefensiveBuildingUpgrades.Queries;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Application.DefensiveBuildings.Commands;

public class InitDefensiveBuildingsCommand : IRequest<List<DefensiveBuilding>>
{
    public Guid PlayerId { get; internal set; }
}

public class InitDefensiveBuildingsCommandHandler(
        IContext context,
        IRequestHandler<GetDefensiveBuildingUpgradesQuery, List<DefensiveBuildingUpgrade>> getResourceBuildingUpgradesCommandHandler)
    : IRequestHandler<InitDefensiveBuildingsCommand, List<DefensiveBuilding>>
{
    private readonly IContext _context = context;
    private readonly IRequestHandler<GetDefensiveBuildingUpgradesQuery, List<DefensiveBuildingUpgrade>>
        _getDefensiveBuildingUpgradesCommandHandler = getResourceBuildingUpgradesCommandHandler;

    private List<DefensiveBuildingUpgrade> _defensiveBuildingUpgrades = [];

    public async Task<List<DefensiveBuilding>> Handle(
        InitDefensiveBuildingsCommand request, CancellationToken cancellationToken)
    {
        await GetDefensiveBuildingUpgrades(cancellationToken);
        List<DefensiveBuilding> defensiveBuildings = CreateDefensiveBuildingInstances(request);
        await InsertDefensiveBuildings(defensiveBuildings, cancellationToken);
        return defensiveBuildings;
    }

    private async Task GetDefensiveBuildingUpgrades(CancellationToken cancellationToken)
    {
        var query = new GetDefensiveBuildingUpgradesQuery()
        {
            Level = 0,
            DefensiveBuildingTypes =
                [DefensiveBuildingType.Tower,
                DefensiveBuildingType.Wall]
        };
        _defensiveBuildingUpgrades = await _getDefensiveBuildingUpgradesCommandHandler.Handle(query, cancellationToken);
    }

    private List<DefensiveBuilding> CreateDefensiveBuildingInstances(InitDefensiveBuildingsCommand request)
    {
        var tower = CreateDefensiveBuildingInstance(request, DefensiveBuildingType.Tower);
        var wall = CreateDefensiveBuildingInstance(request, DefensiveBuildingType.Wall);
        return [tower, wall];
    }

    private DefensiveBuilding CreateDefensiveBuildingInstance(InitDefensiveBuildingsCommand request, DefensiveBuildingType defensiveBuildingType)
    {
        var defensiveBuildingUpgrade = _defensiveBuildingUpgrades.First(a=>a.DefensiveBuildingType == defensiveBuildingType);
        return new DefensiveBuilding()
        {
            Id = Guid.NewGuid(),
            Attack = new Attack()
            {
                AgainstCavalry = defensiveBuildingUpgrade.BonusAttack,
                AgainstFlyingCavalry = defensiveBuildingUpgrade.BonusAttack,
                AgainstMage = defensiveBuildingUpgrade.BonusAttack,
                AgainstMeleeInfantry = defensiveBuildingUpgrade.BonusAttack,
                AgainstRangeInfantry = defensiveBuildingUpgrade.BonusAttack,
                AgainstSiegeUnit = defensiveBuildingUpgrade.BonusAttack,
            },
            Defence = new Defense()
            {
                AgainstCavalry = defensiveBuildingUpgrade.BonusDefense,
                AgainstFlyingCavalry = defensiveBuildingUpgrade.BonusDefense,
                AgainstMage = defensiveBuildingUpgrade.BonusDefense,
                AgainstMeleeInfantry = defensiveBuildingUpgrade.BonusDefense,
                AgainstRangeInfantry = defensiveBuildingUpgrade.BonusDefense,
                AgainstSiegeUnit = defensiveBuildingUpgrade.BonusDefense
            },
            HitPoint = defensiveBuildingUpgrade.BonusHitPoint,
            RepairCost = defensiveBuildingUpgrade.RepairCost,
            Level = 0,
            Name = $"Level 0 {defensiveBuildingType}",
            Player = new PlayerReference() { Id = request.PlayerId },
            DefensiveBuildingType = defensiveBuildingType,
            IsUnderUpgrade = false,
            
        };
    }

    private async Task InsertDefensiveBuildings(List<DefensiveBuilding> defensiveBuildings, CancellationToken cancellationToken)
    {
        await _context.DefensiveBuildings.AddRangeAsync(defensiveBuildings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
