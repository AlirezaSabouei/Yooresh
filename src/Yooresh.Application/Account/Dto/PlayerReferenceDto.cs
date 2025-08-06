using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Account;

namespace Yooresh.Application.Account.Dto;

public class PlayerReferenceDto : IMapFrom<PlayerReference>
{
    public Guid Id { get; set; }
}
