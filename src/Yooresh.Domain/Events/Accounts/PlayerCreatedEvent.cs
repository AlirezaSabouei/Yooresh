using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Events.Accounts;

public class PlayerCreatedEvent(Player player) : BaseEvent
{
    public Player Player { get; } = player;
}
