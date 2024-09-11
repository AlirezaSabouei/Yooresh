namespace Yooresh.Domain.Exceptions;

public class DomainException : Exception
{
    protected DomainException(string error) : base(error)
    {
    }
}