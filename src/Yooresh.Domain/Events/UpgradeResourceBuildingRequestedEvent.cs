namespace Yooresh.Domain.Events;

public class UpgradeResourceBuildingRequestedEvent : BaseEvent
{
    public Guid ResourceBuildingId { get; }

    public UpgradeResourceBuildingRequestedEvent(Guid villageId,Guid resourceBuildingId)
    {
        ResourceBuildingId = resourceBuildingId;
    }
}
