namespace Yooresh.Domain.Entities.Factions;

public class Faction : RootEntity
{
    private string Advantages { get; set; }
    private string Disadvantages { get; set; }

    public string Name { get; set; } = null!;
    public IReadOnlyCollection<string> AdvantagesList => Advantages.Split(",");
    public IReadOnlyCollection<string> DisadvantagesList => Disadvantages.Split(",");

    #region Ef Core Required Methods

    private Faction()
    {
    }

    #endregion

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