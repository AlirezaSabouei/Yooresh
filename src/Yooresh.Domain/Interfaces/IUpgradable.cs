using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Interfaces;

public interface IUpgradableTemp
{
    public string UpgradeName { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; }

    #region These props are commented out for the sake of easy development. They will be added in the next phase

     public bool UpgradeInProgress { get;}
     public DateTimeOffset? UpgradeStartTime { get; set; }
     public DateTimeOffset? UpgradeEndTime { get; set; }

    #endregion

    void StartUpgrade();
}

