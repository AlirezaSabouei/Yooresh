using AutoMapper;
using Villages.Application.Common.Mappings;

namespace Villages.Application.Villages.Commands;

public class CreateVillageCommandDto : IMapFrom<CreateVillageCommand>
{
    public string Name { get; set; }
    public Guid FactionId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateVillageCommand, CreateVillageCommandDto>().ReverseMap();
    }
}