using Yooresh.Domain.Entities.Players;

namespace Yooresh.Domain.Events.Accounts;

public class PlayerConfirmedEvent(Player player) : BaseEvent
{
    public Guid PlayerId { get; } = player.Id;
}