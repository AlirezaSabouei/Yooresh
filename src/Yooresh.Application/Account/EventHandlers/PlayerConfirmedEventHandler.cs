using MediatR;
using Microsoft.Extensions.Configuration;
using Yooresh.Application.Resources.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Application.Account.EventHandlers;

public class PlayerConfirmedEventHandler(
    IConfiguration configuration,
    IRequestHandler<CreateResourceCommand, List<Resource>> createResourceCommandHandler)
    : INotificationHandler<PlayerConfirmedEvent>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IRequestHandler<CreateResourceCommand, List<Resource>> _createResourceCommandHandler =
        createResourceCommandHandler;

    public async Task Handle(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        await InitiateResourcesForPlayer(notification, cancellationToken);
    }

    private async Task InitiateResourcesForPlayer(PlayerConfirmedEvent notification, CancellationToken cancellationToken)
    {
        var startingAvailableAmount = int.Parse(_configuration.GetSection("Resources")["StartingAvailableAmount"]!);
        var startingHarvestRatePerMinute = int.Parse(_configuration.GetSection("Resources")["StartingHarvestRatePerMinute"]!);
        CreateResourceCommand command = BuildCreateResourceCommand(
            notification, startingAvailableAmount, startingHarvestRatePerMinute);
        await _createResourceCommandHandler.Handle(command, cancellationToken);
    }

    private CreateResourceCommand BuildCreateResourceCommand(
        PlayerConfirmedEvent notification, int startingAvailableAmount, int startingHarvestRatePerMinute)
    {
        return new()
        {
            PlayerId = notification.PlayerId,
            StartingAvailableAmount = startingAvailableAmount,
            StartingHarvestRatePerMinute = startingHarvestRatePerMinute
        };
    }
}
