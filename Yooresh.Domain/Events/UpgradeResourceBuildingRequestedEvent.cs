using Yooresh.Domain.Common;

namespace Yooresh.Domain.Events;

public class UpgradeResourceBuildingRequestedEvent : BaseEvent
{
    public Guid To { get; }
    public Guid VillageId { get; }

    public UpgradeResourceBuildingRequestedEvent(Guid villageId,Guid to)
    {
        To = to;
        VillageId = villageId;
    }
}