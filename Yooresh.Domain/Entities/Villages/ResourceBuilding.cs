using Yooresh.Domain.Enums;
using Yooresh.Domain.Exceptions;
using Yooresh.Domain.Interfaces;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Villages;

public class ResourceBuilding : BaseEntity, IUpgradable<ResourceBuilding>, IGatherable
{
    public string Name { get; set; }


    public string UpgradeName { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Resource UpgradeCost { get; set; }
    public Guid? TargetId { get; set; }
    public ResourceBuilding? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public bool UpgradeInProgress { get; set; }
    public DateTimeOffset? UpgradeStartTime { get; set; }
    public int Level { get; set; }


    public ResourceType ProductionType { get; set; }
    public Resource HourlyProduction { get; set; }
    public DateTimeOffset? LastResourceGatherDate { get; set; }
    public bool IsFinished => UpgradeInProgress && DateTimeOffset.Now <= UpgradeStartTime!.Value.Add(UpgradeDuration);

    public void GatherProducedResources(Village village)
    {
        var elapsedTimeSinceLastResourceChangeTime = (DateTimeOffset.Now - LastResourceGatherDate!.Value).TotalHours;

        village.Resource += (HourlyProduction * elapsedTimeSinceLastResourceChangeTime);

        LastResourceGatherDate = DateTimeOffset.Now;
    }

    public void StartUpgrade(Village village)
    {
        CheckAvailableResources(village);
        CheckAvailableBuilders(village);
        SendAWorkerToDoTheJob(village);
        UpgradeInProgress = true;
        UpgradeStartTime = DateTimeOffset.Now;
    }

    private void CheckAvailableResources(Village village)
    {
        if (UpgradeCost > village.Resource)
        {
            throw new NotEnoughResourcesException();
        }
    }

    private void CheckAvailableBuilders(Village village)
    {
        if (NeedBuilderForUpgrade && village.AvailableBuilders == 0)
        {
            throw new NotAvailableBuildersException();
        }        
    }

    private void SendAWorkerToDoTheJob(Village village)
    {
        if (NeedBuilderForUpgrade)
        {
            village.AvailableBuilders -= 1;
        }
    }
}