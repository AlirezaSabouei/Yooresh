using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.ResourceBuildings;

public class ResourceBuildingUpgrade : BaseEntity
{
    public int Level { get; set; }
    public ResourceBuildingType ResourceBuildingType { get; set; }
    public string UpgradeName { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
    public int BonusHarvestRatePerMinute { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
}
