using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Domain.Interfaces;

public interface ITrainable
{
    public ResourceValueObject TrainCost { get; set; }
    public TimeSpan TrainDuration { get; set; }
   // public IUpgradable Target { get; set; }
    public void StartTrain();
}
