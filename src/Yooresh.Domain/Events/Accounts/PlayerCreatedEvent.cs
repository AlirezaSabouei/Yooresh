using Yooresh.Domain.Entities.Account;

namespace Yooresh.Domain.Events.Accounts;

public class PlayerCreatedEvent(Player player) : BaseEvent
{
    public Player Player { get; } = player;
}
