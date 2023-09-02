using Eloy.Domain.Entities.Resources;

namespace Eloy.Domain.Entities.ResourceBuildings;

public abstract class ResourceBuilding : BaseEntity
{
    public abstract string Name { get; }
    public abstract ResourceBuildingType BuildingType { get;  }
    public abstract Resource ProducedResource { get; }
    public abstract int UpgradeResourceCost { get;  }
    public abstract int UpgradeTime { get;  }
    public abstract List<Resource> UpgradeResources { get; }
    
    
    public int Level { get; set; }
    public int ProductionRatePerHour { get; set; }
}