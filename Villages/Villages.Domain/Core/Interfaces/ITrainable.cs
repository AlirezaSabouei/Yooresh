using Villages.Domain.Common.ValueObjects;

namespace Villages.Domain.Core.Interfaces;

public interface ITrainable
{
    public Resource TrainCost { get; set; }
    public TimeSpan TrainDuration { get; set; }
    // public IUpgradable Target { get; set; }
    public void StartTrain();
}
