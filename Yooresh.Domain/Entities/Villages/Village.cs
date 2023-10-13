using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Villages;

public class Village : RootEntity
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Player Player { get; set; }
    public Guid FactionId { get; set; }
    public Faction Faction { get; set; }
    public Resource Resource { get; set; }
    public int AvailableBuilders { get; set; }
    public List<ResourceBuilding> ResourceBuildings { get; set; }

    public Village()
    {
        Resource = new Resource(0, 0, 0, 0, 0);
        AvailableBuilders = 2;
        ResourceBuildings=new List<ResourceBuilding>();
    }

    public Village GatherResources()
    {
        foreach (var resourceBuilding in ResourceBuildings)
        {
            resourceBuilding.GatherProducedResources(this);
        }
        
        return this;
    }

    /*public Village ApplyFinishedUpgrades()
    {
        var newResourceBuildings = new List<ResourceBuilding>();
        foreach (var resourceBuilding in ResourceBuildings)
        {
            if (resourceBuilding.UpgradeInProgress && resourceBuilding.IsFinished)
            {
                var newResourceBuilding = resourceBuilding.Target!;
                newResourceBuilding.LastResourceGatherDate = resourceBuilding.LastResourceGatherDate;
                newResourceBuildings.Add(newResourceBuilding);
            }
        }
        ResourceBuildings.RemoveAll(x => x.UpgradeInProgress && x.IsFinished);
        ResourceBuildings.AddRange(newResourceBuildings);

        return this;
    }*/
}