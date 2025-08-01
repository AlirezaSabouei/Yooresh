namespace Yooresh.Domain.Exceptions;

public class NotAvailableTowerCapacityException : DomainException
{
    private const string Error = "Not available capacity to load troops";

    public NotAvailableTowerCapacityException() : base(Error)
    {
    }
}