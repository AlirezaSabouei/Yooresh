using System.Linq.Expressions;

namespace Villages.Domain.Core.Interfaces;

public interface IJob
{
    public void QueueJob(Expression<Action> action, TimeSpan delay);
}