namespace Yooresh.Domain.Common.Events;

public class UpgradeResourceBuildingRequestedEvent : BaseEvent
{
    public Guid ResourceBuildingId { get; }
    public Guid VillageId { get; }

    public UpgradeResourceBuildingRequestedEvent(Guid villageId, Guid resourceBuildingId)
    {
        VillageId = villageId;
        ResourceBuildingId = resourceBuildingId;
    }
}