using Villages.Domain.Common.ValueObjects;

namespace Villages.Domain.Core.Entities;

public partial class Village
{
    public Resource Resource { get; set; } = new Resource(0, 0, 0, 0, 0);
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