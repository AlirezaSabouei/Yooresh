namespace Eloy.DTO;

public class VillageResourceBuildingDto
{
    public Guid Id { get; set; }
    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }

    public Guid ResourceBuildingId { get; set; }
    public ResourceBuilding ResourceBuilding { get; set; }

    public DateTimeOffset LastHarvestTime { get; set; }
}
