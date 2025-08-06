using Yooresh.Domain.Entities.Account;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Domain.Entities.ResourceBuildings;

public class ResourceBuilding : BaseEntity,IUpgradableTemp
{
    public string Name { get; set; }
    public ResourceBuildingType ResourceBuildingType { get; set; }
    public int Level { get; set; }
    public int HarvestRatePerMinute { get; set; }
    public PlayerReference Player { get; set; }


    public string UpgradeName { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public bool UpgradeInProgress => UpgradeEndTime.HasValue && UpgradeEndTime < DateTime.UtcNow;
    public DateTimeOffset? UpgradeStartTime { get; set; }
    public DateTimeOffset? UpgradeEndTime { get; set; }
    bool IUpgradableTemp.UpgradeInProgress { get => UpgradeInProgress; set => throw new NotImplementedException(); }


    // public void StartUpgrade()
    // {
    //    AddDomainEvent(new UpgradeResourceBuildingRequestedEvent());
    // }

    // private void CheckAvailableResources(Village village)
    //  {
    //  if (Target!.UpgradeCost > village.Resource)
    //  {
    //  throw new NotEnoughResourcesException();
    // }
    // }

    //private void CheckAvailableBuilders(Village village)
    //{
    //    if (NeedBuilderForUpgrade && village.AvailableBuilders == 0)
    //    {
    //        throw new NotAvailableBuildersException();
    //    }
    //}

    //private void SendAWorkerToDoTheJob(Village village)
    //{
    //    if (NeedBuilderForUpgrade)
    //    {
    //        village.AvailableBuilders -= 1;
    //    }
    //}
}
