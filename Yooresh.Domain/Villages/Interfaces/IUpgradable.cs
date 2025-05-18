using Yooresh.Domain.Common.Entities;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Villages.Interfaces;

public interface IUpgradable<T> where T : BaseEntity
{
    public string UpgradeName { get; }
    public Resource UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public T? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; }
    public int Level { get; set; }

    #region These props are commented out for the sake of easy development. They will be added in the next phase

    // public bool UpgradeInProgress { get; set; }
    // public DateTimeOffset? UpgradeStartTime { get; set; }
    //

    //
    // public bool IsUpgradeFinished => UpgradeInProgress && DateTimeOffset.Now <= UpgradeStartTime!.Value.Add(ResourceBuilding.UpgradeDuration);

    #endregion
}
