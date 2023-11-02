using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Application.Villages.Dto;

public class VillageBuildingDto : IMapFrom<VillageBuilding>
{
    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }

    public Guid ResourceBuildingId { get; set; }
    public BuildingDto ResourceBuilding { get; set; }

    public DateTimeOffset LastHarvestTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VillageBuilding, VillageBuildingDto>()
            .ReverseMap();
    }
}