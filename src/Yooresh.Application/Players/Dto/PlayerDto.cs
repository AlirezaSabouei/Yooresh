using Yooresh.Application.Common;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Players.Dto;

public class PlayerDto : BaseDto, IMapFrom<Player>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public RoleDto Role { get; set; }
    public bool Confirmed { get; set; }
}