/*using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities.Resources.Extends;

namespace Yooresh.Domain.Entities.ResourceBuildings.Extends;

public class GoldMine : ResourceBuilding
{
    public override string Name => nameof(GoldMine);
    public override ResourceBuildingType BuildingType => ResourceBuildingType.GoldMine;
    public override Resource ProducedResource => new Gold();
    public override int UpgradeResourceCost => Level * 100;
    public override int UpgradeTime => Level * 100;

    public override List<Resource> UpgradeResources =>
        new()
        {
            new Stone(),
            new Metal(),
            new Lumber()
        };
}*/