namespace Yooresh.Client.WinForms.Models;

public class Faction
{
    public Guid Id { get; set; }
    private List<string> Advantages { get; set; }
    private List<string> Disadvantages { get; set; }

    public string Name { get; set; } = null!;
}
