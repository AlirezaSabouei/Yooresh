using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Entities.Armies;

public class Army : RootEntity
{
    public PlayerReference Player { get; set; }
    public List<Squad> Squads { get; set; }
}
