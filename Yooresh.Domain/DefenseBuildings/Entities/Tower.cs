using Yooresh.Domain.Buildings.Entities;
using Yooresh.Domain.Common.ValueObjects;

namespace Yooresh.Domain.DefenseBuildings.Entities;

public class Tower : Building
{
    public override string Name => Level == 0 ? "Damaged tower" : $"Tower {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to tower {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Defense Defense { get; set; }
    public int Health { get; set; }
    public Resource RepairCost { get; set; }
    public int TroopCapacity { get; set; }

    public Tower()
    {
        BuildingType = BuildingType.Tower;
    }
}