using AutoMapper;
using Villages.Application.Common.Mappings;

namespace Villages.Application.Villages.Commands;

public class UpgradeResourceBuildingCommandDto:IMapFrom<UpdateResourceBuildingCommand>
{
    public Guid ResourceBuildingId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateResourceBuildingCommand, UpgradeResourceBuildingCommandDto>().ReverseMap();
    }
}