using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Players;

namespace Yooresh.Application.Players.Dto;

public class PlayerDto : IMapFrom<Player>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public bool Confirmed { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, PlayerDto>()
            .ReverseMap();
    }
}