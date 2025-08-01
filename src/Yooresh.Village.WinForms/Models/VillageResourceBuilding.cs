using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yooresh.Client.WinForms.Models;

public class VillageResourceBuilding
{
    public Guid Id { get; set; }
    public Guid VillageId { get; set; }
    public Village Village { get; set; }

    public Guid ResourceBuildingId { get; set; }
    public ResourceBuilding ResourceBuilding { get; set; }

    public DateTimeOffset LastHarvestTime { get; set; }
}
