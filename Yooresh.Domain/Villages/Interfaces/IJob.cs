using System.Linq.Expressions;

namespace Yooresh.Domain.Villages.Interfaces;

public interface IJob
{
    public void QueueJob(Expression<Action> action, TimeSpan delay);
}