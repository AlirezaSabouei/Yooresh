namespace Yooresh.Domain.Entities.Accounts;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string .Empty;
    public Player Player { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
}
