using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Buildings.Entities;

namespace Villages.Domain.Core.DefenseBuildings.Entities;

public class Wall : Building
{
    public override string Name => Level == 0 ? "Broken wall" : $"Wall {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to wall {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public virtual Defense Defense { get; set; }
    public int Health { get; set; }
    public Resource RepairCost { get; set; }

    public Wall()
    {
        BuildingType = BuildingType.Wall;
    }
}