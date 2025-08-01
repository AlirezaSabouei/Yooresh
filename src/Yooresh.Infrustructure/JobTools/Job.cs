using System.Linq.Expressions;
using Hangfire;
using Yooresh.Domain.Interfaces;

namespace Yooresh.Infrastructure.JobTools;

public class Job : IJob
{
    public void QueueJob(Expression<Action> action, TimeSpan delay)
    {
        BackgroundJob.Schedule(action, delay);
    }
}