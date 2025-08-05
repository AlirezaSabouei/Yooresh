using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Application.Villages.Dto;

public class VillageWallDto : IMapFrom<VillageWall>
{
    public Guid Id { get; set; }
    public Guid WallId { get; set; }
    public WallDto Wall { get; set; }

    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }
}
