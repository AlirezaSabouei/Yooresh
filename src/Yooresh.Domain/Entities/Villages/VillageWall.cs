using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.Domain.Entities.Villages;

public class VillageWall : BaseEntity
{
    public Guid WallId { get; set; }
    public Wall Wall { get; set; }

    public Guid VillageId { get; set; }
    public Village Village { get; set; }
}