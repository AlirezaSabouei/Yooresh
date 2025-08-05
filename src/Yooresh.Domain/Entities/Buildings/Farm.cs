using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.Buildings;

public class Farm : Building
{
    public override string Name => Level == 0 ? "Burnt farm" : $"Farm {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to farm {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public ResourceValueObject HourlyProduction { get; set; }

    public Farm()
    {
        BuildingType = BuildingType.Farm;
    }
}