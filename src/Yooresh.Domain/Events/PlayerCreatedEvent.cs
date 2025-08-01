using Yooresh.Domain.Entities.Players;

namespace Yooresh.Domain.Events;

public class PlayerCreatedEvent(Player player) : BaseEvent
{
    public Player Player { get; } = player;
}