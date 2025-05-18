using System.Collections.ObjectModel;
using Villages.Domain.Common.Entities;
using Villages.Domain.Core.DefenseBuildings.Entities;
using Villages.Domain.Core.Troops;

namespace Villages.Domain.Core.Entities;

public class VillageTower : BaseEntity
{
    public Guid TowerId { get; set; }
    public Tower Tower { get; set; }

    public Guid VillageId { get; set; }
    public Village Village { get; set; }

    private List<Troop> _troops = new();
    public ReadOnlyCollection<Troop> LoadedTroops { get; set; }

    public void LoadTroopIntoTower(Troop troop)
    {
        if (LoadedTroops.Count == Tower.TroopCapacity)
        {
            throw new NotAvailableTowerCapacityException();
        }

        _troops.Add(troop);
    }
}