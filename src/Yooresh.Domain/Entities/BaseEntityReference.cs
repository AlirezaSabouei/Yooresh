using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Yooresh.Domain.Entities;

public abstract class BaseEntityReference
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
}
