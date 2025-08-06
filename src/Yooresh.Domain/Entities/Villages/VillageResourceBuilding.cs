using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.Domain.Entities.Villages;

public class VillageBuilding : BaseEntity
{
    public Guid VillageId { get; set; }
    public Village Village { get; set; }

    public Guid BuildingId { get; set; }
    public Building Building { get; set; }

    public DateTimeOffset LastHarvestTime { get; set; }

    public VillageBuilding()
    {
    }

    public VillageBuilding(Village village, Building building)
    {
        VillageId = village.Id;
        BuildingId = building.Id;
        LastHarvestTime = DateTimeOffset.UtcNow;
    }

    public void GatherProducedResources(Village village)
    {
        if (Building.BuildingType is BuildingType.Farm or BuildingType.Lumbermill or BuildingType.StoneMine or BuildingType.GoldMine)
        {
            //var elapsedTimeSinceLastResourceChangeTime = (DateTimeOffset.UtcNow - LastHarvestTime).TotalHours;
            //var building = Building as Farm;//It does not matter you can cast to any resource building. All of them support resource gathering
            //village.Resource += building!.HourlyProduction * elapsedTimeSinceLastResourceChangeTime;
            //LastHarvestTime = DateTimeOffset.UtcNow;
        }
    }
}