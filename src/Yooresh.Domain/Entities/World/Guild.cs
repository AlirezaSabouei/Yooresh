using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Entities.World;

public class Guild : BaseEntity
{
    public Guild()
    {
        Members = new List<Player>();
    }

    public string Name { get; set; } = null!;
    public Guid AreaId { get; set; }
    public Area Area { get; set; } = null!;
    public Guid GuildMasterId { get; set; }
    public Player GuildMaster { get; set; } = null!;
    public List<Player> Members { get; set; } = null!;
}