using Eloy.Domain.Entities.Resources;
using Eloy.Domain.Entities.Resources.Extends;

namespace Eloy.Domain.Entities.ResourceBuildings.Extends;

public class StoneMine : ResourceBuilding
{
    public override string Name => nameof(StoneMine);
    public override ResourceBuildingType BuildingType => ResourceBuildingType.StoneMine;
    public override Resource ProducedResource => new Stone();
    public override int UpgradeResourceCost => Level * 100;
    public override int UpgradeTime => Level * 100;

    public override List<Resource> UpgradeResources =>
        new()
        {
            new Metal(),
            new Lumber()
        };
}