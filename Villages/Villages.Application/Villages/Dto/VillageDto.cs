using Villages.Application.Common.Mappings;
using Villages.Application.Factions.Dto;
using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Entities;

namespace Villages.Application.Villages.Dto;

public class VillageDto:IMapFrom<Village>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerReferenceDto Player { get; set; }
    public Guid FactionId { get; set; }
    public FactionDto Faction { get; set; }
    public Resource Resource { get; set; }
    public int AvailableBuilders { get; set; }
    public DateTimeOffset LastResourceGatherDate { get; set; }
    public List<VillageBuildingDto> VillageBuildings { get; set; }

    public VillageWallDto VillageWall { get; set; }
    public VillageTowerDto VillageTower { get; set; }
}
