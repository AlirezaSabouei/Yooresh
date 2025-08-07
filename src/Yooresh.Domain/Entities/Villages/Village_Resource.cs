using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.Villages;

public partial class Village
{
    public ResourceValueObject Resource { get; set; } = new ResourceValueObject(0,0,0,0);
    public List<VillageBuilding> VillageResourceBuildings { get; set; } = new();

    public Village GatherResources()
    {
        foreach (var villageResourceBuilding in VillageResourceBuildings)
        {
            villageResourceBuilding.GatherProducedResources(this);
        }

        return this;
    }
}