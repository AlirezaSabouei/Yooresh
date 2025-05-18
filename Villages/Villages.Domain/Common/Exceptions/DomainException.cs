namespace Villages.Domain.Common.Exceptions;

public class DomainException : Exception
{
    protected DomainException(string error) : base(error)
    {
    }
}