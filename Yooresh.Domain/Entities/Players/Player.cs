namespace Yooresh.Domain.Entities.Players;

public class Player : RootEntity
{
    private string _email = null!;
    public string Name { get; private set; } = null!;

    public string Email
    {
        get => _email;
        private set
        {
            _email = value.ToLower();
        }
    }

    public string Password { get; private set; } = null!;
    public Role Role { get; private set; }
    public bool Confirmed { get; private set; }

    private Player()
    {
        Id = Guid.NewGuid();
    }
    public Player(string name, string email, string password, Role role)
    {
        Id=Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public virtual void ConfirmPlayer()
    {
        Confirmed = true;
    }
}
