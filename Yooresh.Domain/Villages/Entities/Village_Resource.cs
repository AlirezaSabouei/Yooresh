using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.Villages.Entities;

namespace Yooresh.Domain.Entities.Villages;

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