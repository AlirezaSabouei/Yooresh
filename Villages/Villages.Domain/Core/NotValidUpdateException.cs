using Villages.Domain.Common.Exceptions;

namespace Villages.Domain.Core;

public class NotValidUpdateException : DomainException
{
    private const string Error = "Not Valid Update";

    public NotValidUpdateException() : base(Error)
    {
    }
}