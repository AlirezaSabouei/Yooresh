using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Buildings;

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