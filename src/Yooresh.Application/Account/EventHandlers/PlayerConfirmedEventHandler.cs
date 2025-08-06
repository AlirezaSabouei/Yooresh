using MediatR;
using Yooresh.Application.ResourceBuildings.Commands;
using Yooresh.Application.Resources.Commands;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Application.Account.EventHandlers;

public class PlayerConfirmedEventHandler(
    IRequestHandler<InitResourcesCommand, List<Resource>> initResourcesCommandHandler,
    IRequestHandler<InitResourceBuildingsCommand, List<ResourceBuilding>> initResourceBuildingsCommandHandler)
    : INotificationHandler<PlayerConfirmedEvent>
{
    private readonly IRequestHandler<InitResourcesCommand, List<Resource>> _initResourceCommandHandler =
        initResourcesCommandHandler;
    private readonly IRequestHandler<InitResourceBuildingsCommand, List<ResourceBuilding>> _initResourceBuildingsCommandHandler =
        initResourceBuildingsCommandHandler;

    public async Task Handle(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        await InitiateResourcesForPlayer(notification, cancellationToken);
        await InitiateResourceBuildingsForPlayer(notification, cancellationToken);
    }

    private async Task InitiateResourcesForPlayer(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        InitResourcesCommand command = BuildInitResourcesCommand(notification);
        await _initResourceCommandHandler.Handle(command, cancellationToken);
    }

    private InitResourcesCommand BuildInitResourcesCommand(PlayerConfirmedEvent notification)
    {
        return new()
        {
            PlayerId = notification.PlayerId
        };
    }

    private async Task InitiateResourceBuildingsForPlayer(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        InitResourceBuildingsCommand command = BuildInitResourceBuildingsCommand(notification);
        await _initResourceBuildingsCommandHandler.Handle(command, cancellationToken);
    }

    private InitResourceBuildingsCommand BuildInitResourceBuildingsCommand(PlayerConfirmedEvent notification)
    {
        return new()
        {
            PlayerId = notification.PlayerId
        };
    }
}
