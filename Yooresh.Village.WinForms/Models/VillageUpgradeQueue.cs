namespace Yooresh.Client.WinForms.Models;

public class VillageUpgradeQueue
{
    public Guid? FromId { get; set; }
    public Guid ToId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool Completed { get; set; }
    public UpgradeType UpgradeType { get; set; }
    public bool IsFinished => (EndTime <= DateTimeOffset.Now) && !Completed;
}