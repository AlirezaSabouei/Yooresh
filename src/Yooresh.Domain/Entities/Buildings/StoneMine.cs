using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Entities.Buildings;

public class StoneMine : Building
{
    public override string Name => Level == 0 ? "Damaged stone mine" : $"Stone mine {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to stone mine {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public ResourceValueObject HourlyProduction { get; set; }

    public StoneMine()
    {
        BuildingType = BuildingType.StoneMine;
    }
}