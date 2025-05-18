using Yooresh.Domain.Common.Exceptions;

namespace Yooresh.Domain.Exceptions;

public class NotAvailableBuildersException : DomainException
{
    private const string Error = "Not available builders";

    public NotAvailableBuildersException() : base(Error)
    {
    }
}