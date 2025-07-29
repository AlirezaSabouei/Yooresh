using MediatR;
using Microsoft.Extensions.Configuration;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Events;

namespace Yooresh.Application.Players.EventHandlers;

public class PlayerCreatedEventHandler(
    IEmail email,
    IConfiguration configuration) 
    : INotificationHandler<PlayerCreatedEvent>
{
    private readonly IEmail _email = email;
    private readonly IConfiguration _configuration = configuration;

    public async Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        var message = _configuration.GetSection("Email")["Message"];
        message = message!
            .Replace("@code", notification.Player.ConfirmationCode);

        _email.SendEmailAsync(notification.Player.Email, "Activate Your Yooresh Account", message);
    }
}
