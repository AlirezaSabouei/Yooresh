namespace Yooresh.Village.Models.Resources;

public class ResourceModel
{
    public ResourceType ResourceType { get; set; }
    public int AvailableAmount { get; set; }
    public int HarvestRatePerMinute { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
