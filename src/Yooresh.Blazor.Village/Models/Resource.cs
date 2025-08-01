namespace Yooresh.Village.Models;

public class Resource
{
    public ResourceType ResourceType { get; set; }
    public int AvailableAmount { get; set; }
    public int HarvestRatePerMinute { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
