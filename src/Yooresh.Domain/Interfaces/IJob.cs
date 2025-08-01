using System.Linq.Expressions;

namespace Yooresh.Domain.Interfaces;

public interface IJob
{
    public void QueueJob(Expression<System.Action> action, TimeSpan delay);
}