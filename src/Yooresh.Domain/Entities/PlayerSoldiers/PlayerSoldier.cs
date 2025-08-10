using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.Soldiers;

namespace Yooresh.Domain.Entities.PlayerSoldiers;

public class PlayerSoldier : RootEntity
{
    public PlayerReference Player { get; set; }
    public SoldierReference Soldier { get; set; }
    public string Name { get; set; }
    public int HitPoint { get; set; }
    public int MoveSpeed { get; set; }
    public SoldierType SoldierType { get; set; }
    public int TrainDurationInMinuts { get; set; }
}
