using Yooresh.Domain.Entities.Players;

namespace Yooresh.Domain.Entities.Villages;

public class Village : BaseEntity
{
    private const int STARTING_AMOUNT = 100;
    public string Name { get; private set; } = null!;
    public Faction Faction { get; private set; } = null!;
    public PlayerReference Player { get; private set; } = null!;
    public Resource Resource { get; private set; } = null!;

    public List<Troop> Troops { get; set; }

    //For EF
    private Village(){ }

    public Village(string name,Faction faction,PlayerReference player,Resource? villageResource=null)
    {
        Name = name;
        Faction = faction;
        Player = player;
        Resource = villageResource ?? new Resource(0,0,0,0,0);
    }

    public void AddInitialResources()
    {
        Resource = new Resource(STARTING_AMOUNT,STARTING_AMOUNT,STARTING_AMOUNT,STARTING_AMOUNT,STARTING_AMOUNT);
    }

    public void AddResource(Resource resource)
    {
        Resource += resource;
    }
}

public class Troop:ValueObject
{
    public string Name { get; set; }
    public int Power { get; set; }
}