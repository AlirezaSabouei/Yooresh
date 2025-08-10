using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Domain.Entities.Armies;

public class Army : RootEntity
{
    public PlayerReference Player { get; set; }
    public List<Squad> Squads { get; set; }
}
