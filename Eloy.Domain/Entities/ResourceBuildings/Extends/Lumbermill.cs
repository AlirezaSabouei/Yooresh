using Eloy.Domain.Entities.Resources;
using Eloy.Domain.Entities.Resources.Extends;

namespace Eloy.Domain.Entities.ResourceBuildings.Extends;

public class Lumbermill : ResourceBuilding
{
    public override string Name => nameof(Lumbermill);
    public override ResourceBuildingType BuildingType => ResourceBuildingType.LumberMill;
    public override Resource ProducedResource => new Lumber();
    public override int UpgradeResourceCost => Level * 100;
    public override int UpgradeTime => Level * 100;

    public override List<Resource> UpgradeResources =>
        new()
        {
            new Stone(),
            new Metal()
        };
}