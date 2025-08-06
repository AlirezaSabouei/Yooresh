namespace Yooresh.Domain.Entities.Account;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Player Player { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
}
