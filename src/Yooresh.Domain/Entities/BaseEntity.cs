using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Yooresh.Domain.Events;

namespace Yooresh.Domain.Entities;

[BsonIgnoreExtraElements]
public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTimeOffset CreateDate { get; set; }

    public DateTimeOffset UpdateDate { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    [BsonIgnore]
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
