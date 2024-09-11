using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Application.Villages.Dto;

public class VillageTowerDto : IMapFrom<VillageTower>
{
    public Guid Id { get; set; }
    public Guid TowerId { get; set; }
    public TowerDto Tower { get; set; }

    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VillageTower, VillageTowerDto>()
            .ReverseMap();
    }
}
