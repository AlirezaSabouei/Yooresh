namespace Yooresh.Domain.Entities.Villages;

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

   internal Resource(int food,int lumber,int stone,int gold,int metal)
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
        villageResource.Food = a.Food+b.Food;
        villageResource.Lumber = a.Lumber + b.Lumber;
        villageResource.Metal = a.Metal + b.Metal;
        villageResource.Stone = a.Stone + b.Stone;
        villageResource.Gold = a.Gold + b.Gold;
        return villageResource;
    }
}