using AutoMapper;
using Yooresh.Application.Common.Mappings;

namespace Yooresh.Application.Villages.Commands;

public class UpgradeResourceBuildingCommandDto:IMapFrom<UpdateResourceBuildingCommand>
{
    public Guid ToId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateResourceBuildingCommand, UpgradeResourceBuildingCommandDto>().ReverseMap();
    }
}