using Eloy.Domain.Entities.Resources;
using Eloy.Domain.Entities.Resources.Extends;

namespace Eloy.Domain.Entities.ResourceBuildings.Extends;

public class Farm : ResourceBuilding
{
    public override string Name => nameof(Farm);
    public override ResourceBuildingType BuildingType => ResourceBuildingType.Farm;
    public override Resource ProducedResource => new Crop();
    public override int UpgradeResourceCost => Level * 100;
    public override int UpgradeTime => Level * 100;

    public override List<Resource> UpgradeResources =>
        new()
        {
            new Stone(),
            new Metal(),
            new Lumber()
        };
}