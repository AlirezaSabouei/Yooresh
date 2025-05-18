using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Common.Events;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Application.Villages.EventHandlers;

public class UpgradeResourceBuildingRequestedEventHandler : INotificationHandler<UpgradeResourceBuildingRequestedEvent>
{
    private readonly IContext _context;
    private readonly ILogger<UpgradeResourceBuildingRequestedEventHandler> _logger;
    private readonly IJob _job;

    public UpgradeResourceBuildingRequestedEventHandler(IContext context,
        ILogger<UpgradeResourceBuildingRequestedEventHandler> logger, IJob job)
    {
        _context = context;
        _logger = logger;
        _job = job;
    }

    public async Task Handle(UpgradeResourceBuildingRequestedEvent notification, CancellationToken cancellationToken)
    {
        var resourceBuilding = await _context.Buildings
            .Include(a => a.Target)
            .SingleAsync(a => a.Id == notification.ResourceBuildingId, cancellationToken);
        _job.QueueJob(()=>UpdateTheBuilding(notification,cancellationToken),resourceBuilding.UpgradeDuration);
    }

    public async Task UpdateTheBuilding(UpgradeResourceBuildingRequestedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        var village = await _context.Villages
            .SingleAsync(a => a.Id == notification.VillageId, cancellationToken);

        var building = await _context.Buildings
            .Include(a => a.Target)
            .SingleAsync(a => a.Id == notification.ResourceBuildingId, cancellationToken);

        village.VillageResourceBuildings.RemoveAll(a => a.BuildingId == notification.ResourceBuildingId);

        _context.VillageBuildings.Add(new VillageBuilding(village, building.Target));

        if (building.NeedBuilderForUpgrade)
        {
            village.AvailableBuilders += 1;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}