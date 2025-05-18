using Villages.Domain.Common.Entities;
using Villages.Domain.Core.DefenseBuildings.Entities;

namespace Villages.Domain.Core.Entities;

public class VillageWall : BaseEntity
{
    public Guid WallId { get; set; }
    public Wall Wall { get; set; }

    public Guid VillageId { get; set; }
    public Village Village { get; set; }
}