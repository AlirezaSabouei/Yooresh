using Villages.Application.Common.Mappings;
using Villages.Domain.Players.Entities;

namespace Villages.Application.Players.Dto;

public class PlayerReferenceDto : IMapFrom<PlayerReference>
{
    public Guid Id { get; set; }
}