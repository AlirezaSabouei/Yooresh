using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Buildings.Entities;

namespace Villages.Domain.Core.ResourceBuildings.Entities;

public class Farm : Building
{
    public override string Name => Level == 0 ? "Burnt farm" : $"Farm {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to farm {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Resource HourlyProduction { get; set; }

    public Farm()
    {
        BuildingType = BuildingType.Farm;
    }
}