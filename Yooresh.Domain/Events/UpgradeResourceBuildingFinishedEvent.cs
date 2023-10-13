namespace Yooresh.Domain.Events;

public class UpgradeResourceBuildingFinishedEvent : BaseEvent
{
    public Guid To { get; }
    public Guid VillageId { get; }

    public UpgradeResourceBuildingFinishedEvent(Guid to,Guid villageId)
    {
        To = to;
        VillageId = villageId;
    }
}