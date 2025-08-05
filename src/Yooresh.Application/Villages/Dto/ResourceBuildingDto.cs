using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Villages.Dto;

public class BuildingDto : IMapFrom<Building>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
    public string UpgradeName { get; set; }
    public int Level { get; set; }
    public Guid? TargetId { get; set; }
    public BuildingDto? Target { get; set; }
    public ResourceValueObject HourlyProduction { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Building, BuildingDto>()
            .ReverseMap();
    }
}