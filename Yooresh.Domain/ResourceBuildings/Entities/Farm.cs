using Yooresh.Domain.Buildings.Entities;
using Yooresh.Domain.Common.ValueObjects;

namespace Yooresh.Domain.ResourceBuildings.Entities;

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