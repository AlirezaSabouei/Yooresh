using Yooresh.Domain.Entities.Account;

namespace Yooresh.Domain.Entities.Resources;

public class Resource() : BaseEntity
{
    public required PlayerReference Player { get; set; }
    public ResourceType ResourceType { get; set; }
    public int AvailableAmount { get; set; }
    public int HarvestRatePerMinute { get; set; }
    public DateTime LastUpdateDate { get; set; }

    public void CalculateAvailableAmount(DateTime now)
    {
        if (now <= LastUpdateDate) { return; }
        int duration = (int)(now - LastUpdateDate).TotalMinutes;
        AvailableAmount += (duration * HarvestRatePerMinute);
    }
}
