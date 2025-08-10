using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.DefensiveBuildingUpgrades;

public class DefensiveBuildingUpgrade : BaseEntity
{
    public int Level { get; set; }
    public DefensiveBuildingType DefensiveBuildingType { get; set; }
    public string UpgradeName { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
    public ResourceValueObject RepairCost { get; set; }
    public int BonusAttack { get; set; }
    public int BonusDefense { get; set; }
    public int BonusHitPoint { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
}
