using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.Soldiers;

namespace Yooresh.Domain.Entities.MilitiaBuildings;

public class MilitiaBuilding : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public MilitiaBuildingType MilitiaBuildingType { get; set; }
    public int Level { get; set; } = 0;
    public required PlayerReference Player { get; set; }
    public bool IsUnderUpgrade { get; set; } = false;
    public List<SoldierReference> Soldiers { get; set; } = [];
}
