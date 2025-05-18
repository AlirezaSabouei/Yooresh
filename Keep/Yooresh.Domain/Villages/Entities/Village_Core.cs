using Yooresh.Domain.Common.Entities;
using Yooresh.Domain.Players;
using Yooresh.Domain.Villages.Factions.Entities;

namespace Yooresh.Domain.Entities.Villages;

public partial class Village : RootEntity
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Player Player { get; set; }
    public Guid FactionId { get; set; }
    public Faction Faction { get; set; }
    public int AvailableBuilders { get; set; } = 2;
}