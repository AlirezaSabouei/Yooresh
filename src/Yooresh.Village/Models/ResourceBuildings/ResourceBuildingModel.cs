namespace Yooresh.Village.Models.ResourceBuildings;

public class ResourceBuildingModel
{
    public string Name { get; set; } = string.Empty;
    public ResourceBuildingTypeModel ResourceBuildingType { get; set; }
    public int Level { get; set; } = 0;
    public int HarvestRatePerMinute { get; set; } = 0;
    public bool IsUnderUpgrade { get; set; } = false;
}
