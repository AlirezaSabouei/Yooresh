namespace Yooresh.Domain.Common.ValueObjects;

public class Defense : ValueObject
{
    public int AgainstMeleeInfantry { get; set; } = 0;
    public int AgainstRangeInfantry { get; set; } = 0;
    public int AgainstCavalry { get; set; } = 0;
    public int AgainstMage { get; set; } = 0;
    public int AgainstFlyingCavalry { get; set; } = 0;
    public int AgainstSiegeUnit { get; set; } = 0;

    public Defense()
    {

    }
    public Defense(
        int againstMeleeInfantry,
        int againstRangeInfantry,
        int againstCavalry,
        int againstMage,
        int againstFlyingCavalry,
        int againstSiegeUnit)
    {
        AgainstMeleeInfantry = againstMeleeInfantry;
        AgainstRangeInfantry = againstRangeInfantry;
        AgainstCavalry = againstCavalry;
        AgainstMage = againstMage;
        AgainstFlyingCavalry = againstFlyingCavalry;
        AgainstSiegeUnit = againstSiegeUnit;
    }
    public static Defense operator +(Defense a, Defense b)
    {
        Defense defense = new()
        {
            AgainstMeleeInfantry = a.AgainstMeleeInfantry + b.AgainstMeleeInfantry,
            AgainstRangeInfantry = a.AgainstRangeInfantry + b.AgainstRangeInfantry,
            AgainstCavalry = a.AgainstCavalry + b.AgainstCavalry,
            AgainstMage = a.AgainstMage + b.AgainstMage,
            AgainstFlyingCavalry = a.AgainstFlyingCavalry + b.AgainstFlyingCavalry,
            AgainstSiegeUnit = a.AgainstSiegeUnit + b.AgainstSiegeUnit
        };

        return defense;
    }

    public static Defense operator *(Defense a, double number)
    {
        Defense defense = new()
        {
            AgainstMeleeInfantry = Convert.ToInt32(a.AgainstMeleeInfantry * number),
            AgainstRangeInfantry = Convert.ToInt32(a.AgainstRangeInfantry * number),
            AgainstCavalry = Convert.ToInt32(a.AgainstCavalry * number),
            AgainstMage = Convert.ToInt32(a.AgainstMage * number),
            AgainstFlyingCavalry = Convert.ToInt32(a.AgainstFlyingCavalry * number),
            AgainstSiegeUnit = Convert.ToInt32(a.AgainstSiegeUnit * number),
        };
        return defense;
    }
}