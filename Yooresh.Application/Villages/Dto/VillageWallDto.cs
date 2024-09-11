using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Application.Factions.Dto;
using Yooresh.Application.Players.Dto;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;
using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.Application.Villages.Dto;

public class VillageWallDto : IMapFrom<VillageWall>
{
    public Guid Id { get; set; }
    public Guid WallId { get; set; }
    public WallDto Wall { get; set; }

    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VillageWall, VillageWallDto>()
            .ReverseMap();
    }
}
