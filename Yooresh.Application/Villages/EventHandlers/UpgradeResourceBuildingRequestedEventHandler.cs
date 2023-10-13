using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Events;

namespace Yooresh.Application.Villages.EventHandlers;

public class UpgradeResourceBuildingRequestedEventHandler:INotificationHandler<UpgradeResourceBuildingRequestedEvent>
{
    private readonly IContext _context;
    private readonly ILogger<UpgradeResourceBuildingRequestedEventHandler> _logger;
    
    public UpgradeResourceBuildingRequestedEventHandler(IContext context,ILogger<UpgradeResourceBuildingRequestedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(UpgradeResourceBuildingRequestedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        var village =await _context.Villages
            .SingleAsync(a => a.Id == notification.VillageId,cancellationToken);

        var resourceBuilding = await _context.ResourceBuildings
            .Include(a=>a.Target)
            .SingleAsync(a => a.Id == notification.ResourceBuildingId,cancellationToken);

        village.ResourceBuildings.RemoveAll(a => a.Id == notification.ResourceBuildingId);
        
        village.ResourceBuildings.Add(resourceBuilding.Target!);

        await _context.SaveChangesAsync(cancellationToken);
    }
}