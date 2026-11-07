using MediatR;
using Microsoft.Extensions.Logging;
using Yooresh.Domain.Events;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Application.ResourceBuildings.EventHandlers;

public class UpgradeResourceBuildingRequestedEventHandler : INotificationHandler<UpgradeResourceBuildingRequestedEvent>
{
    private readonly ILogger<UpgradeResourceBuildingRequestedEventHandler> _logger;
    private readonly IJob _job;

    public UpgradeResourceBuildingRequestedEventHandler(
        ILogger<UpgradeResourceBuildingRequestedEventHandler> logger, IJob job)
    {
        _logger = logger;
        _job = job;
    }

    public Task Handle(UpgradeResourceBuildingRequestedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
