using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Entities.WarLogs;

public class WarLog : RootEntity
{
    public PlayerReference Player { get; set; }
    public PlayerReference Attacker { get; set; }
    public DateTime AttackDate { get; set; }
    //Log of soldiers and the result
}
