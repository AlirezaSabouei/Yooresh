namespace Yooresh.Domain.Events.Accounts;

public class PlayerConfirmedEvent(Guid playerId) : BaseEvent
{
    public Guid PlayerId { get; } = playerId;
}