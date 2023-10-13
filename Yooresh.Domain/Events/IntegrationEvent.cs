namespace Yooresh.Domain.Events;

public class IntegrationEvent
{
    public DateTime Timestamp { get; }

    protected IntegrationEvent()
    {
        Timestamp = DateTime.UtcNow;
    }
}