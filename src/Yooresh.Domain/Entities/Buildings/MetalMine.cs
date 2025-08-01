using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Buildings;

public class MetalMine : Building
{
    public override string Name => Level == 0 ? "Damaged metal mine" : $"Metal mine {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to metal mine {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Resource HourlyProduction { get; set; }

    public MetalMine()
    {
        BuildingType = BuildingType.MetalMine;
    }
}