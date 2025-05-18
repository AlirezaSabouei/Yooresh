namespace Users.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = Guid.NewGuid().ToString();
    public DateTime Expires { get; set; }
    public DateTime? Revoked { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsExpired && !IsRevoked;
}