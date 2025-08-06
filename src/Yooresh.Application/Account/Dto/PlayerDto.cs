using Yooresh.Application.Common;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Account;

namespace Yooresh.Application.Account.Dto;

public class PlayerDto : BaseDto, IMapFrom<Player>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public RoleDto Role { get; set; }
    public bool Confirmed { get; set; }
}