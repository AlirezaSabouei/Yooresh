using Villages.Domain.Common.Entities;
using Villages.Domain.Common.Events;
using Villages.Domain.Common.Exceptions;
using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Entities;
using Villages.Domain.Core.Interfaces;

namespace Villages.Domain.Core.Buildings.Entities;

public abstract class Building : BaseEntity, IUpgradable<Building>
{
    public abstract string Name { get; }
    public BuildingType BuildingType { get; protected set; }
    public abstract string UpgradeName { get; }
    public abstract bool NeedBuilderForUpgrade { get; }
    public Resource UpgradeCost { get; set; } = new(0, 0, 0, 0, 0);
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public Building? Target { get; set; }
    public int Level { get; set; }


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
}