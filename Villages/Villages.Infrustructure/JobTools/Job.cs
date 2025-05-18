using System.Linq.Expressions;
using Hangfire;
using Villages.Domain.Core.Interfaces;

namespace Villages.Infrastructure.JobTools;

public class Job : IJob
{
    public void QueueJob(Expression<Action> action, TimeSpan delay)
    {
        BackgroundJob.Schedule(action, delay);
    }
}