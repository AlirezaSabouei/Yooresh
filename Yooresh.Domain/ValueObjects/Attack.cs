namespace Yooresh.Domain.ValueObjects;

public class Attack
{
    public int AgainstMeleeInfantry { get; set; }
    public int AgainstRangeInfantry { get; set; }
    public int AgainstCavalry { get; set; }
    public int AgainstMage { get; set; }
    public int AgainstFlyingCavalry { get; set; }
    public int AgainstSiegeUnit { get; set; }
}