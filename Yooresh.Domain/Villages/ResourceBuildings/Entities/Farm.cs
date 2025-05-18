using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.Villages.Buildings.Entities;

namespace Yooresh.Domain.Villages.ResourceBuildings.Entities;

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