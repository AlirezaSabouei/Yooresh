using Yooresh.Application.Common;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Dto;

public class PlayerDto : BaseDto, IMapFrom<Player>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public RoleDto Role { get; set; }
    public bool Confirmed { get; set; }
}