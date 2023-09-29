using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.Application.ResourceBuildings.Dto;

public class ResourceBuildingDto : IMapFrom<ResourceBuilding>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Resource UpgradeCost { get; set; }
    public int Level { get; set; }
    public Guid? RequirementId { get; set; }
    public ResourceBuildingDto? Requirement { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ResourceBuilding, ResourceBuildingDto>()
            .ReverseMap();
    }
}