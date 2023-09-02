using Eloy.Domain.Entities.Resources;
using Eloy.Domain.Entities.Resources.Extends;

namespace Eloy.Domain.Entities.ResourceBuildings.Extends;

public class MetalMine : ResourceBuilding
{
    public override string Name => nameof(MetalMine);
    public override ResourceBuildingType BuildingType => ResourceBuildingType.MetalMine;
    public override Resource ProducedResource => new Metal();
    public override int UpgradeResourceCost => Level * 100;
    public override int UpgradeTime => Level * 100;

    public override List<Resource> UpgradeResources =>
        new()
        {
            new Stone(),
            new Lumber()
        };
}