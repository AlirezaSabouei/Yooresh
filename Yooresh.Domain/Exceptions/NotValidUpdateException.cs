using Yooresh.Domain.Common.Exceptions;

namespace Yooresh.Domain.Exceptions;

public class NotValidUpdateException : DomainException
{
    private const string Error = "Not Valid Update";

    public NotValidUpdateException() : base(Error)
    {
    }
}