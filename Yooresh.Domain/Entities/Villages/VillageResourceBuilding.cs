using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.Domain.Entities.Villages;

public class VillageResourceBuilding : BaseEntity
{
    public Guid VillageId { get; private set; }
    public Village Village { get; private set; }
    public Guid ResourceBuildingId { get; private set; }
    public ResourceBuilding ResourceBuilding { get; private set; }


    private VillageResourceBuilding()
    {
        
    }

    public VillageResourceBuilding(Guid villageId,Guid resourceBuildingId)
    {
        Id = Guid.NewGuid();
        VillageId = villageId;
        ResourceBuildingId = resourceBuildingId;
    }
}