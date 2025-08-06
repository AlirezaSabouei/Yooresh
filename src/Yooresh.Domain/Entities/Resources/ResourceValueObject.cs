using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Entities.Resources;

public class ResourceValueObject : ValueObject
{
    public int Gold { get; private set; }
    public int Lumber { get; private set; }
    public int Stone { get; private set; }
    public int Food { get; private set; }

    protected ResourceValueObject()
    {
        
    }

    public ResourceValueObject(int gold,int lumber,int stone,int food)
    {
        Gold = gold;
        Lumber = lumber;
        Stone = stone;
        Food = food;
    }

    public static ResourceValueObject operator +(ResourceValueObject a, ResourceValueObject b)
    {
        ResourceValueObject villageResource = new()
        {
            Food = a.Food + b.Food,
            Lumber = a.Lumber + b.Lumber,
            Stone = a.Stone + b.Stone,
            Gold = a.Gold + b.Gold
        };
        return villageResource;
    }

    public static ResourceValueObject operator *(ResourceValueObject a, double number)
    {
        ResourceValueObject villageResource = new()
        {
            Food = Convert.ToInt32(a.Food * number),
            Lumber = Convert.ToInt32(a.Lumber * number),
            Stone = Convert.ToInt32(a.Stone * number),
            Gold = Convert.ToInt32(a.Gold * number)
        };
        return villageResource;
    }

    public static bool operator >(ResourceValueObject resourceCost, ResourceValueObject currentResource)
    {
        return resourceCost.Food > currentResource.Food &&
               resourceCost.Lumber > currentResource.Lumber &&
               resourceCost.Stone > currentResource.Stone &&
               resourceCost.Gold > currentResource.Gold;
    }

    public static bool operator <(ResourceValueObject resourceCost, ResourceValueObject currentResource)
    {
        return resourceCost.Food < currentResource.Food ||
               resourceCost.Lumber < currentResource.Lumber ||
               resourceCost.Stone < currentResource.Stone ||
               resourceCost.Gold < currentResource.Gold;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ResourceValueObject)obj;

        return other.Gold == Gold &&
            other.Lumber == Lumber &&
            other.Stone == Stone &&
            other.Food == Food;
    }

    public static bool operator ==(ResourceValueObject? a, ResourceValueObject? b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(ResourceValueObject? a, ResourceValueObject? b)
    {
        return !(a == b);
    }
}