using Villages.Domain.Common.Exceptions;

namespace Villages.Domain.Core;

public class NotAvailableTowerCapacityException : DomainException
{
    private const string Error = "Not available capacity to load troops";

    public NotAvailableTowerCapacityException() : base(Error)
    {
    }
}