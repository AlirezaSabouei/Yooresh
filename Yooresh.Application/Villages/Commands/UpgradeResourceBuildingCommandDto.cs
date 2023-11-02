using AutoMapper;
using Yooresh.Application.Common.Mappings;

namespace Yooresh.Application.Villages.Commands;

public class UpgradeResourceBuildingCommandDto:IMapFrom<UpdateResourceBuildingCommand>
{
    public Guid ResourceBuildingId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateResourceBuildingCommand, UpgradeResourceBuildingCommandDto>().ReverseMap();
    }
}