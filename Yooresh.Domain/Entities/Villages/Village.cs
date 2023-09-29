using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Exceptions;

namespace Yooresh.Domain.Entities.Villages;

public class Village : RootEntity
{
    private List<VillageResourceBuilding> _resourceBuildings;
    private List<VillageUpgradeQueue> _upgrades;

    public string Name { get; private set; }
    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; }
    public Guid FactionId { get; private set; }
    public Faction Faction { get; private set; }
    public Resource Resource { get; private set; }
    public int AvailableBuilders { get; private set; }
    public DateTimeOffset LastResourceChangeTime { get; private set; }
    public List<VillageResourceBuilding> ResourceBuildings { get; private set; } //=> _resourceBuildings.AsReadOnly();
    public List<VillageUpgradeQueue> Upgrades { get; private set; } //=> _upgrades.AsReadOnly();

    #region Ef Core Methods

    //For EF
    private Village()
    {
        _resourceBuildings = new List<VillageResourceBuilding>();
        _upgrades = new List<VillageUpgradeQueue>();
    }

    #endregion

    public Village(string name, Guid factionId, Guid playerId)
    {
        Name = name;
        AvailableBuilders = 2;
        FactionId = factionId;
        PlayerId = playerId;
        Resource = new Resource(0, 0, 0, 0, 0);
        LastResourceChangeTime = DateTimeOffset.Now;
        _resourceBuildings = new List<VillageResourceBuilding>();
        _upgrades = new List<VillageUpgradeQueue>();
    }

    public void StartUpgrade(Guid? from, Guid to, TimeSpan duration, UpgradeType type, Resource cost)
    {
        CheckAvailableResources(cost);
        CheckAvailableBuilders(type);
        UpgradeResource(cost);
        SendAWorkerToDoTheJob(type);
        var endTime = DateTimeOffset.Now.Add(duration);
        var upgrade = new VillageUpgradeQueue(DateTimeOffset.Now, endTime, from, to, type);
        _upgrades.Add(upgrade);
    }

    private void CheckAvailableResources(Resource cost)
    {
        if (cost > Resource)
        {
            throw new NotEnoughResourcesException();
        }
    }

    private void CheckAvailableBuilders(UpgradeType type)
    {
        if (type == UpgradeType.ResourceBuilding && AvailableBuilders == 0)
        {
            throw new NotAvailableBuildersException();
        }
    }

    private void SendAWorkerToDoTheJob(UpgradeType type)
    {
        if (type == UpgradeType.ResourceBuilding)
        {
            AvailableBuilders -= 1;
        }
    }

    public void UpgradeResource(Resource resourceToAdd)
    {
        Resource += resourceToAdd;
        LastResourceChangeTime = DateTimeOffset.Now;
    }

    public void RefreshVillage()
    {
        GatherResource();
        CheckFinishedUpgrades();
    }

    private void GatherResource()
    {
        if (!_resourceBuildings.Any())
        {
            return;
        }

        var elapsedTimeSinceLastResourceChangeTime = (DateTimeOffset.Now - LastResourceChangeTime).TotalHours;

        foreach (var resourceBuilding in _resourceBuildings)
        {
            Resource += (resourceBuilding.ResourceBuilding.HourlyProduction * elapsedTimeSinceLastResourceChangeTime);
        }

        LastResourceChangeTime = DateTimeOffset.Now;
    }

    private void CheckFinishedUpgrades()
    {
        var completedUpgrades = _upgrades.Where(a =>!a.Completed && a.IsFinished);

        foreach (var completedUpgrade in completedUpgrades)
        {
            completedUpgrade.Completed = true;
            switch (completedUpgrade.UpgradeType)
            {
                case UpgradeType.ResourceBuilding:
                    UpdateResourceBuildings(completedUpgrade.FromId,completedUpgrade.ToId);
                    break;
                case UpgradeType.Troop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void UpdateResourceBuildings(Guid? from,Guid to)
    {
        var index = -1;
        if (from.HasValue)
        {
            index = _resourceBuildings.FindIndex(a => a.Id == from);
        }
        var villageBuilding = new VillageResourceBuilding(Id, to);
        if (index == -1)
        {
            _resourceBuildings.Add(villageBuilding);
        }
        else
        {
            _resourceBuildings[index] = villageBuilding;
        }
    }
}