namespace Yooresh.Domain.ValueObjects;

public class Attack : ValueObject
{
    public int AgainstMeleeInfantry { get; set; } = 0;
    public int AgainstRangeInfantry { get; set; } = 0;
    public int AgainstCavalry { get; set; } = 0;
    public int AgainstMage { get; set; } = 0;
    public int AgainstFlyingCavalry { get; set; } = 0;
    public int AgainstSiegeUnit { get; set; } = 0;

    public static Attack operator +(Attack a, Attack b)
    {
        Attack attack = new()
        {
            AgainstMeleeInfantry = a.AgainstMeleeInfantry + b.AgainstMeleeInfantry,
            AgainstRangeInfantry = a.AgainstRangeInfantry + b.AgainstRangeInfantry,
            AgainstCavalry = a.AgainstCavalry + b.AgainstCavalry,
            AgainstMage = a.AgainstMage + b.AgainstMage,
            AgainstFlyingCavalry = a.AgainstFlyingCavalry + b.AgainstFlyingCavalry,
            AgainstSiegeUnit = a.AgainstSiegeUnit + b.AgainstSiegeUnit
        };

        return attack;
    }

    public static Attack operator *(Attack a, double number)
    {
        Attack attack = new()
        {
            AgainstMeleeInfantry = Convert.ToInt32(a.AgainstMeleeInfantry * number),
            AgainstRangeInfantry = Convert.ToInt32(a.AgainstRangeInfantry * number),
            AgainstCavalry = Convert.ToInt32(a.AgainstCavalry * number),
            AgainstMage = Convert.ToInt32(a.AgainstMage * number),
            AgainstFlyingCavalry = Convert.ToInt32(a.AgainstFlyingCavalry * number),
            AgainstSiegeUnit = Convert.ToInt32(a.AgainstSiegeUnit * number),
        };
        return attack;
    }
}