using Yooresh.Domain.Common.Exceptions;

namespace Yooresh.Domain.Villages;

public class NotAvailableTowerCapacityException : DomainException
{
    private const string Error = "Not available capacity to load troops";

    public NotAvailableTowerCapacityException() : base(Error)
    {
    }
}