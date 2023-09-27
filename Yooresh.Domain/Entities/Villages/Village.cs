using Yooresh.Domain.Entities.Players;

namespace Yooresh.Domain.Entities.Villages;

public class Village : RootEntity
{
    public string Name { get; private set; }
    public Guid FactionId { get; private set; }
    public Faction Faction { get; private set; }
    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; }
    public Resource Resource { get; private set; }

    //For EF
    private Village()
    {
    }

    public Village(string name, Guid factionId, Guid playerId)
    {
        Id = Guid.NewGuid();
        Name = name;
        FactionId = factionId;
        PlayerId = playerId;
        Resource = new Resource(0, 0, 0, 0, 0);
    }

    public void AddResource(Resource resource)
    {
        Resource += resource;
    }
}