namespace Yooresh.Domain.Entities.Soldiers;

public class Soldier : BaseEntity
{
    public string Name { get; set; }
    public int HitPoint { get; set; }
    public int MoveSpeed { get; set; }
    public SoldierType SoldierType { get; set; }
    public int TrainDurationInMinuts { get; set; }
}
