using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Events;
using Yooresh.Domain.Exceptions;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Domain.Entities.Buildings;

public abstract class Building:BaseEntity,IUpgradableTemp
{
    public abstract string Name { get;}
    public BuildingType BuildingType { get; protected set; }

    public abstract string UpgradeName { get;}
    public abstract bool NeedBuilderForUpgrade { get;}

    public ResourceValueObject UpgradeCost { get; set; } = new (0, 0, 0, 0);
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public Building? Target { get; set; }
    public int Level { get; set; }
    string IUpgradableTemp.UpgradeName { get => UpgradeName; set => throw new NotImplementedException(); }
    bool IUpgradableTemp.NeedBuilderForUpgrade { get => NeedBuilderForUpgrade; set => throw new NotImplementedException(); }
    public bool UpgradeInProgress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTimeOffset? UpgradeStartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTimeOffset? UpgradeEndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void StartUpgrade(Village village)
    {
        if (TargetId == null)
            return;
        CheckAvailableResources(village);
        CheckAvailableBuilders(village);
        SendAWorkerToDoTheJob(village);
        AddDomainEvent(new UpgradeResourceBuildingRequestedEvent(village.Id, Id));
    }

    private void CheckAvailableResources(Village village)
    {
        if (Target!.UpgradeCost > village.Resource)
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

    public void StartUpgrade()
    {
        throw new NotImplementedException();
    }
}