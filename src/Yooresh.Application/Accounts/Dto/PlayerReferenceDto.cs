using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Dto;

public class PlayerReferenceDto : IMapFrom<PlayerReference>
{
    public Guid Id { get; set; }
}
