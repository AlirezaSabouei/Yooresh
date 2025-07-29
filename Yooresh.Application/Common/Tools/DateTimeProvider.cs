namespace Yooresh.Application.Common.Tools;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
