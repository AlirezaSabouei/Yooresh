using Yooresh.Domain.Entities.Soldiers;

namespace Yooresh.Domain.Entities.Armies;

public class Squad : BaseEntity
{
    public Guid ArmyId { get; set; }
    public SoldierType SoldierType { get; set; }
    public int Count { get; set; }
}
