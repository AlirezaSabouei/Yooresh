using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Domain.Entities.Account;

public class Player : RootEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.SimplePlayer;
    public bool Confirmed { get; set; } = false;
    public string ConfirmationCode { get; set; } = Guid.NewGuid().ToString();
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    protected Player()
    {
    }

    public Player(string name, string email, string password)
    {
        Name = name;
        Email = email.ToLower();
        Password = password;
    }

    public virtual void ConfirmPlayer(string confirmationCode)
    {
        if (ConfirmationCode == confirmationCode)
        {
            AddDomainEvent(new PlayerConfirmedEvent(Id));
            Confirmed = true;
        }
    }
}