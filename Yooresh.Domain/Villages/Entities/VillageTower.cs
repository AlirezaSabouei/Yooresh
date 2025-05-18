using System.Collections.ObjectModel;
using Yooresh.Domain.DefenseBuildings.Entities;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Troops;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Exceptions;

namespace Yooresh.Domain.Villages.Entities;

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