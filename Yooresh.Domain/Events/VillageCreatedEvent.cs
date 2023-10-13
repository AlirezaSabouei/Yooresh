namespace Yooresh.Domain.Events;

public class VillageCreatedEvent:IntegrationEvent
{
    public string Mame { get; set; }
}