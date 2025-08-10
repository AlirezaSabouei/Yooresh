using MediatR;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Application.DefensiveBuildingUpgrades.Queries;
using Yooresh.Application.ResourceBuildingUpgrades.Queries;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;

namespace Yooresh.Application.ResourceBuildings.Commands;

public class InitResourceBuildingsCommand : IRequest<List<ResourceBuilding>>
{
    public Guid PlayerId { get; internal set; }
}

public class InitResourceBuildingsCommandHandler(
        IContext context,
        IRequestHandler<GetResourceBuildingUpgradesQuery, List<ResourceBuildingUpgrade>> getResourceBuildingUpgradesCommandHandler,
        IDateTimeProvider dateTimeProvider)
    : IRequestHandler<InitResourceBuildingsCommand, List<ResourceBuilding>>
{
    private readonly IContext _context = context;
    private readonly IRequestHandler<GetResourceBuildingUpgradesQuery, List<ResourceBuildingUpgrade>>
        _getResourceBuildingUpgradesCommandHandler = getResourceBuildingUpgradesCommandHandler;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    private List<ResourceBuildingUpgrade> _resourceBuildingUpgrades = [];

    public async Task<List<ResourceBuilding>> Handle(
        InitResourceBuildingsCommand request, CancellationToken cancellationToken)
    {
        await GetResourceBuildingUpgrades(cancellationToken);
        List<ResourceBuilding> resourcesBuildings = CreateResourceBuildingInstances(request);
        await InsertResourceBuildings(resourcesBuildings, cancellationToken);
        return resourcesBuildings;
    }

    private async Task GetResourceBuildingUpgrades(CancellationToken cancellationToken)
    {
        var query = new GetResourceBuildingUpgradesQuery()
        {
            Level = 0,
            ResourceBuildingTypes =
                [ResourceBuildingType.GoldMine,
                ResourceBuildingType.LumberMill,
                ResourceBuildingType.StoneMine,
                ResourceBuildingType.Farm]
        };
        _resourceBuildingUpgrades = await _getResourceBuildingUpgradesCommandHandler.Handle(query, cancellationToken);
    }

    private List<ResourceBuilding> CreateResourceBuildingInstances(InitResourceBuildingsCommand request)
    {
        var goldMine = CreateResourceBuildingInstance(request, ResourceBuildingType.GoldMine);
        var lumberMill = CreateResourceBuildingInstance(request, ResourceBuildingType.LumberMill);
        var stoneMine = CreateResourceBuildingInstance(request, ResourceBuildingType.StoneMine);
        var farm = CreateResourceBuildingInstance(request, ResourceBuildingType.Farm);
        return [goldMine, lumberMill, stoneMine, farm];
    }

    private ResourceBuilding CreateResourceBuildingInstance(InitResourceBuildingsCommand request, ResourceBuildingType resourceBuildingType)
    {
        var resourceBuildingUpgrade = _resourceBuildingUpgrades.First(a=>a.ResourceBuildingType == resourceBuildingType);
        return new ResourceBuilding()
        {
            Id = Guid.NewGuid(),
            HarvestRatePerMinute = resourceBuildingUpgrade.BonusHarvestRatePerMinute,
            Level = 0,
            Name = $"Level 0 {resourceBuildingType}",
            Player = new PlayerReference() { Id = request.PlayerId },
            ResourceBuildingType = resourceBuildingType,
            IsUnderUpgrade = false
        };
    }

    private async Task InsertResourceBuildings(List<ResourceBuilding> resourceBuildings, CancellationToken cancellationToken)
    {
        await _context.ResourceBuildings.AddRangeAsync(resourceBuildings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
