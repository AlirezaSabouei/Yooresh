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

    protected Player()
    {
        RefreshTokens = [];
        Confirmed = false;
        ConfirmationCode = Guid.NewGuid().ToString();
        Role = Role.SimplePlayer;
    }

    public Player(string name, string email, string password)
    {
        RefreshTokens = [];
        Confirmed = false;
        ConfirmationCode = Guid.NewGuid().ToString();
        Role = Role.SimplePlayer;

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