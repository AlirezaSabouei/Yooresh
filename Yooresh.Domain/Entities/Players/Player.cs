namespace Yooresh.Domain.Entities.Players;

public class Player : RootEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public bool Confirmed { get; private set; }

    public Player(string name, string email, string password, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email.ToLower();
        Password = password;
        Role = role;
    }
    
    public Player(string name, string email, string password, Role role,Guid id)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email.ToLower();
        Password = password;
        Role = role;
        Id=id;
    }

    public virtual void ConfirmPlayer()
    {
        Confirmed = true;
    }
}