namespace Yooresh.Village.Models.Accounts;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public bool Confirmed { get; set; }
}
