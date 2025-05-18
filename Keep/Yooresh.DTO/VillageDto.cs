namespace Eloy.DTO;

public class VillageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerDto Player { get; set; }
    public Guid FactionId { get; set; }
    public FactionDto Faction { get; set; }
    public ResourceDto Resource { get; set; }
    public int AvailableBuilders { get; set; }
    public DateTimeOffset LastResourceGatherDate { get; set; }
    public List<VillageBuildingDto> VillageBuildings { get; set; }

    public VillageWallDto VillageWall { get; set; }
    public VillageTowerDto VillageTower { get; set; }
}
