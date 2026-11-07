using MongoDB.Bson.Serialization.Attributes;

namespace Yooresh.Domain.Entities.Factions;

public class Faction : RootEntity
{
    [BsonElement("Advantages")]
    public string Advantages { get; set; } = string.Empty;

    [BsonElement("Disadvantages")]
    public string Disadvantages { get; set; } = string.Empty;

    public string Name { get; set; } = null!;

    [BsonIgnore]
    public IReadOnlyCollection<string> AdvantagesList =>
        Advantages.Split(",", StringSplitOptions.RemoveEmptyEntries);

    [BsonIgnore]
    public IReadOnlyCollection<string> DisadvantagesList =>
        Disadvantages.Split(",", StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// Required by the MongoDB serializer.
    /// </summary>
    public Faction()
    {
    }

    public Faction(string name, IEnumerable<string> advantages, IEnumerable<string> disadvantages,
        Guid? id = null)
    {
        if (id.HasValue)
        {
            Id = id.Value;
        }
        Name = name;
        Advantages = string.Join(",", advantages);
        Disadvantages = string.Join(",", disadvantages);
    }
}
