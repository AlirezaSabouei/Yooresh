using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Buildings.Entities;

namespace Villages.Domain.Core.ResourceBuildings.Entities;

public class StoneMine : Building
{
    public override string Name => Level == 0 ? "Damaged stone mine" : $"Stone mine {Level}";
    public override string UpgradeName => Level < 24 ? $"Upgrade to stone mine {Level}" : "";
    public override bool NeedBuilderForUpgrade => true;
    public Resource HourlyProduction { get; set; }

    public StoneMine()
    {
        BuildingType = BuildingType.StoneMine;
    }
}