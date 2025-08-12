using MediatR;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Application.Accounts.EventHandlers;

public class PlayerConfirmedEventHandler(
    IRequestHandler<InitPlayerAssetsCommand> initPlayerAssetsCommandHandler)
    : INotificationHandler<PlayerConfirmedEvent>
{
    private readonly IRequestHandler<InitPlayerAssetsCommand> _initPlayerAssetsCommandHandler =
        initPlayerAssetsCommandHandler;

    public async Task Handle(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        await InitiateResourcesForPlayer(notification, cancellationToken);
    }

    private async Task InitiateResourcesForPlayer(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        InitPlayerAssetsCommand command = BuildInitPlayerAssetsCommand(notification);
        await _initPlayerAssetsCommandHandler.Handle(command, cancellationToken);
    }

    private InitPlayerAssetsCommand BuildInitPlayerAssetsCommand(PlayerConfirmedEvent notification)
    {
        return new()
        {
            PlayerId = notification.PlayerId
        };
    }
}
