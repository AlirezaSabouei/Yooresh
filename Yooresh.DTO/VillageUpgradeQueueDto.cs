namespace Eloy.DTO;

public class VillageUpgradeQueueDto
{
    public Guid? FromId { get; set; }
    public Guid ToId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool Completed { get; set; }
    public UpgradeTypeDto UpgradeType { get; set; }
    public bool IsFinished => (EndTime <= DateTimeOffset.Now) && !Completed;
}