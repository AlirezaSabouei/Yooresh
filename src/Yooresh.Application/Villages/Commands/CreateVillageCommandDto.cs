using AutoMapper;
using Yooresh.Application.Common.Mappings;

namespace Yooresh.Application.Villages.Commands;

public class CreateVillageCommandDto : IMapFrom<CreateVillageCommand>
{
    public string Name { get; set; }
    public Guid FactionId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateVillageCommand, CreateVillageCommandDto>().ReverseMap();
    }
}