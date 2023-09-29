namespace Yooresh.Domain.Entities.Players;

public class Player : RootEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public bool Confirmed { get; private set; }

    //For EF
    private Player() { }

    public Player(string name, string email, string password, Role role,Guid? id=null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email.ToLower();
        Password = password;
        Role = role;
        if(id.HasValue)
        {
            Id=id.Value;
        }
    }

    public virtual void ConfirmPlayer()
    {
        Confirmed = true;
    }
}