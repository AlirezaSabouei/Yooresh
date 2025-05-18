using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.Factions.Entities;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Domain.Entities.Troops;

public class Troop:BaseEntity, ITrainable,IUpgradable<Troop>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Faction Faction { get; set; }
	public Attack Attack { get; set; }
	public Defense Defense { get; set; }
	public SoldierType SoldierType { get; set; }
	public int MovementSpeed { get; set; }
	public int Health { get; set; }
	public int CarryingCapacity { get; set; }
	public Resource TrainCost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public TimeSpan TrainDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public string UpgradeName => throw new NotImplementedException();

	public Resource UpgradeCost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public TimeSpan UpgradeDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Guid? TargetId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Troop? Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public bool NeedBuilderForUpgrade => throw new NotImplementedException();

	public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public void StartTrain()
	{
		throw new NotImplementedException();
	}
}