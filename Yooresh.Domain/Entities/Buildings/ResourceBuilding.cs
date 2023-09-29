using System.Text.Json.Serialization;
using Yooresh.Domain.Common;

namespace Yooresh.Domain.Entities.Buildings;

public class ResourceBuilding : BaseEntity
{
    public string Name { get; }
    public TimeSpan UpgradeDuration { get; }
    public Resource UpgradeCost { get; private set; }
    public Guid? RequirementId { get; set; }
    public ResourceBuilding? Requirement { get; set; }
    public ProductionType ProductionType { get; private set; }
    public Resource HourlyProduction { get; private set; }
    
    private ResourceBuilding()
    {
    }

    public ResourceBuilding(string name, Resource hourlyProduction, Resource upgradeCost,
        ProductionType productionType, TimeSpan upgradeDuration, Guid? id = null,Guid? requirementId=null)
    {
        HourlyProduction = hourlyProduction;
        ProductionType = productionType;
        Name = name;
        UpgradeCost = upgradeCost;
        UpgradeDuration = upgradeDuration;
        if (id.HasValue)
        {
            Id = id.Value;
        }
        if (requirementId.HasValue)
        {
            RequirementId = requirementId;
        }
    }
}