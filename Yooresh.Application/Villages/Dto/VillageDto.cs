using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Application.Factions.Dto;
using Yooresh.Application.Players.Dto;
using Yooresh.Application.ResourceBuildings.Dto;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

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
    public List<ResourceBuildingDto> ResourceBuildings { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Village, VillageDto>()
            .ReverseMap();
    }
}