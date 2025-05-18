using Yooresh.Domain.DefenseBuildings.Entities;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Domain.Villages.Entities;

public class VillageWall : BaseEntity
{
    public Guid WallId { get; set; }
    public Wall Wall { get; set; }

    public Guid VillageId { get; set; }
    public Village Village { get; set; }
}