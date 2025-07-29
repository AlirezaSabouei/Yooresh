namespace Yooresh.Domain.Entities;

public abstract class BaseEntityReference
{
    public Guid Id { get; set; }

    protected BaseEntityReference()
    {
        Id = Guid.NewGuid();
    }
}