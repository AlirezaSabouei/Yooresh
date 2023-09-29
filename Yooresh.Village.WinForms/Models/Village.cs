namespace Yooresh.Client.WinForms.Models;

public class Village
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Player Player { get; set; }
    public Guid FactionId { get; set; }
    public Faction Faction { get; set; }
    public Resource Resource { get; set; }
    public int AvailableBuilders { get; set; }
    public DateTimeOffset LastResourceChangeTime { get; set; }
    public List<VillageResourceBuilding> ResourceBuildings { get; set; }
    public List<VillageUpgradeQueue> Upgrades { get; set; }
}
