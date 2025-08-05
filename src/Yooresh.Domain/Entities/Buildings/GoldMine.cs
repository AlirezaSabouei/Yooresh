using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.Buildings;

public class GoldMine : Building
{
    public override string Name => Level == 0 ? "Damaged gold mine" : $"Gold mine {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to gold mine {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public ResourceValueObject HourlyProduction { get; set; }

    public GoldMine()
    {
        BuildingType = BuildingType.GoldMine;
    }
}