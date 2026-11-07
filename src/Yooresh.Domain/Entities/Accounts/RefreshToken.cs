using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Yooresh.Domain.Entities.Accounts;

[BsonIgnoreExtraElements]
public class RefreshToken
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Refresh tokens are embedded inside the owning <see cref="Player"/> document, so the
    /// back-reference is not persisted (it would otherwise create an infinite document graph).
    /// </summary>
    [BsonIgnore]
    public Player Player { get; set; } = null!;

    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
}
