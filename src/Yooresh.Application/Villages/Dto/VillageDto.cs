using AutoMapper;
using Yooresh.Application.Account.Dto;
using Yooresh.Application.Common.Mappings;
using Yooresh.Application.Factions.Dto;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;
using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.Application.Villages.Dto;

public class VillageDto:IMapFrom<Village>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerDto Player { get; set; }
    public Guid FactionId { get; set; }
    public FactionDto Faction { get; set; }
    public Resource Resource { get; set; }
    public int AvailableBuilders { get; set; }
    public DateTimeOffset LastResourceGatherDate { get; set; }
    public List<VillageBuildingDto> VillageBuildings { get; set; }

    public VillageWallDto VillageWall { get; set; }
    public VillageTowerDto VillageTower { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Village, VillageDto>()
            .ReverseMap();
    }
}
