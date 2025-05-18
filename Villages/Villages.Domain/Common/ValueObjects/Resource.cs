namespace Villages.Domain.Common.ValueObjects;

public class Resource : ValueObject
{
    public int Food { get; private set; }
    public int Lumber { get; private set; }
    public int Stone { get; private set; }
    public int Gold { get; private set; }
    public int Metal { get; private set; }

    private Resource()
    {
    }

    public Resource(int food, int lumber, int stone, int gold, int metal)
    {
        Food = food;
        Lumber = lumber;
        Stone = stone;
        Gold = gold;
        Metal = metal;
    }

    public static Resource operator +(Resource a, Resource b)
    {
        Resource villageResource = new();
        villageResource.Food = a.Food + b.Food;
        villageResource.Lumber = a.Lumber + b.Lumber;
        villageResource.Metal = a.Metal + b.Metal;
        villageResource.Stone = a.Stone + b.Stone;
        villageResource.Gold = a.Gold + b.Gold;
        return villageResource;
    }

    public static Resource operator *(Resource a, double number)
    {
        Resource villageResource = new();
        villageResource.Food = Convert.ToInt32(a.Food * number);
        villageResource.Lumber = Convert.ToInt32(a.Lumber * number);
        villageResource.Metal = Convert.ToInt32(a.Metal * number);
        villageResource.Stone = Convert.ToInt32(a.Stone * number);
        villageResource.Gold = Convert.ToInt32(a.Gold * number);
        return villageResource;
    }

    public static bool operator >(Resource resourceCost, Resource currentResource)
    {
        return resourceCost.Food > currentResource.Food ||
               resourceCost.Lumber > currentResource.Lumber ||
               resourceCost.Stone > currentResource.Stone ||
               resourceCost.Metal > currentResource.Metal ||
               resourceCost.Gold > currentResource.Gold;
    }

    public static bool operator <(Resource resourceCost, Resource currentResource)
    {
        return resourceCost.Food < currentResource.Food ||
               resourceCost.Lumber < currentResource.Lumber ||
               resourceCost.Stone < currentResource.Stone ||
               resourceCost.Metal < currentResource.Metal ||
               resourceCost.Gold < currentResource.Gold;
    }
}