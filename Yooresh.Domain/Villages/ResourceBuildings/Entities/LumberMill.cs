using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.Villages.Buildings.Entities;

namespace Yooresh.Domain.Villages.ResourceBuildings.Entities;

public class LumberMill : Building
{
    public override string Name => Level == 0 ? "Damaged lumbermill" : $"Lumbermill {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to lumbermill {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Resource HourlyProduction { get; set; }

    public LumberMill()
    {
        BuildingType = BuildingType.Lumbermill;
    }
}