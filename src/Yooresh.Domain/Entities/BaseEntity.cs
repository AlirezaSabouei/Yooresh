using System.ComponentModel.DataAnnotations.Schema;
using Yooresh.Domain.Events;

namespace Yooresh.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
