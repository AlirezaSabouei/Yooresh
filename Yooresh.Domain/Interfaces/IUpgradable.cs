using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Interfaces;

public interface IUpgradable<T> where T: BaseEntity
{
    public string UpgradeName { get; set; }
    public Resource UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public T? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public bool UpgradeInProgress { get; set; }
    public DateTimeOffset? UpgradeStartTime { get; set; }
    public bool IsFinished { get; }
    public void StartUpgrade(Village village);
    public int Level { get; set; }
}
