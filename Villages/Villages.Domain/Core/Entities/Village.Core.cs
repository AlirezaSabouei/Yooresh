using Villages.Domain.Common.Entities;
using Villages.Domain.Core.Factions.Entities;
using Villages.Domain.Players.Entities;

namespace Villages.Domain.Core.Entities;

public partial class Village : RootEntity
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerReference Player { get; set; }
    public Guid FactionId { get; set; }
    public Faction Faction { get; set; }
    public int AvailableBuilders { get; set; } = 2;
}