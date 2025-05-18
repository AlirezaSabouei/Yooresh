using Yooresh.Domain.Common.Exceptions;

namespace Yooresh.Domain.Villages;

public class NotValidUpdateException : DomainException
{
    private const string Error = "Not Valid Update";

    public NotValidUpdateException() : base(Error)
    {
    }
}