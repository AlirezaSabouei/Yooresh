using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Exceptions;

namespace Yooresh.Domain.Services;

public class BuildingUpgradeService:IDomainService
{
    public void CheckBuildingUpgradeCost(Village village, ResourceBuilding building)
    {
        if (village.Resource < building.UpgradeCost)
        {
            throw new NotEnoughResourcesException();
        }

       // var from = village.Buildings.FirstOrDefault(a => a.BuildingType == building.BuildingType);
        
       // village.StartUpgrade();
    }
}