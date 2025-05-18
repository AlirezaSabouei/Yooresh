using Yooresh.Domain.Buildings.Entities;
using Yooresh.Domain.Common.ValueObjects;

namespace Yooresh.Domain.ResourceBuildings.Entities;

public class GoldMine : Building
{
    public override string Name => Level == 0 ? "Damaged gold mine" : $"Gold mine {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to gold mine {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Resource HourlyProduction { get; set; }

    public GoldMine()
    {
        BuildingType = BuildingType.GoldMine;
    }
}