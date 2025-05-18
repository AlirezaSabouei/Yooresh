using Villages.Application.Common.Mappings;
using Villages.Domain.Players.Entities;

namespace Villages.Application.Villages.Dto;

public class PlayerReferenceDto : IMapFrom<PlayerReference>
{
    public Guid Id { get; set; }
}
