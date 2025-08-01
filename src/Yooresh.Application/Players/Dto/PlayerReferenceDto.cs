using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Players.Dto;

public class PlayerReferenceDto : IMapFrom<PlayerReference>
{
    public Guid Id { get; set; }
}
