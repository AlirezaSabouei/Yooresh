using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.DefensiveBuildings;

public class DefensiveBuilding : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DefensiveBuildingType DefensiveBuildingType { get; set; }
    public int Level { get; set; } = 0;
    public required PlayerReference Player { get; set; }
    public bool IsUnderUpgrade { get; set; } = false;
    public Defense Defence { get; set; } = new Defense();
    public Attack Attack { get; set; } = new Attack();
    public int HitPoint { get; set; }
    public ResourceValueObject RepairCost { get; set; } = new ResourceValueObject(0, 0, 0, 0);
}
