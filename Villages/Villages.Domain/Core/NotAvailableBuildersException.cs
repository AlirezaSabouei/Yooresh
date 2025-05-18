using Villages.Domain.Common.Exceptions;

namespace Villages.Domain.Core;

public class NotAvailableBuildersException : DomainException
{
    private const string Error = "Not available builders";

    public NotAvailableBuildersException() : base(Error)
    {
    }
}