using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Entities.ResourceBuildings;

public class ResourceBuilding : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ResourceBuildingType ResourceBuildingType { get; set; }
    public int Level { get; set; } = 0;
    public int HarvestRatePerMinute { get; set; } = 0;
    public required PlayerReference Player { get; set; }
    public bool IsUnderUpgrade { get; set; } = false;
}
