using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Domain.Entities.Players;

public class Player : RootEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool Confirmed { get; set; }
    public string ConfirmationCode { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }

    public Player()
    {
        RefreshTokens = [];
    }

    public Player(string name, string email, string password, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email.ToLower();
        Password = password;
        Role = role;
    }

    public virtual void ConfirmPlayer(string confirmationCode)
    {
        if (confirmationCode == ConfirmationCode)
        {
            AddDomainEvent(new PlayerConfirmedEvent(this));
            Confirmed = true;
        }
    }
}