namespace Yooresh.Domain.Entities;

public class Faction:RootEntity
{
    public string Name { get; set; } = null!;
    private List<string> Advantages { get; set; } = null!;
    private List<string> Disadvantages { get; set; } = null!;

    public List<string> AdvantagesList => Advantages;
    public List<string> DisadvantagesList => Disadvantages;
    
    private Faction()
    {
        
    }

    public Faction(string name,List<string> advantages,List<string> disadvantages)
    {
        Name = name;
        Advantages = advantages;
        Disadvantages = disadvantages;
    }
}