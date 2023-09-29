namespace Yooresh.Domain.Entities.Troops;

public class Troop
{
    public int MovementSpeed { get; set; }
    public int Health { get; set; }
    public int DamageAgainstMelee { get; set; }
    public int DamageAgainstRange { get; set; }
    public int DamageAgainstCavalry { get; set; }
    public int DamageAgainstMage { get; set; }
    public int DamageAgainstFlier { get; set; }
    public int CarryingCapacity { get; set; }
}

public class Melee
{
    
}

public class Range
{
    
}

public class Cavalry
{
    
}

public class Mage
{
    
}

public class Flier
{
    
}