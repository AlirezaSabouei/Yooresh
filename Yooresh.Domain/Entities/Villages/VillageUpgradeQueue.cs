using Yooresh.Domain.Common;

namespace Yooresh.Domain.Entities.Villages;

public class VillageUpgradeQueue : BaseEntity
{   
    public Guid? FromId { get; private set; }
    public Guid ToId { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }
    public bool Completed { get; set; }
    public UpgradeType UpgradeType { get; private set; }
    public bool IsFinished=>(EndTime <= DateTimeOffset.Now) && !Completed;


    private VillageUpgradeQueue()
    {
    }

    public VillageUpgradeQueue(DateTimeOffset startTime, DateTimeOffset endTime, Guid? from,Guid to,
        UpgradeType upgradeType)
    {
        StartTime = startTime;
        EndTime = endTime;
        FromId = from;
        ToId = to;
        Completed = false;
        UpgradeType = upgradeType;
    }
}