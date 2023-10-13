using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Enums;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Domain.Interfaces;

public interface IGatherable
{
    public ResourceType ProductionType { get; set; }
    public Resource HourlyProduction { get; set; }
    public DateTimeOffset LastResourceGatherDate { get; set; }

    public void GatherProducedResources(Village village);
}
