namespace Yooresh.Domain.Exceptions;

public class PlayerNotExistsException : DomainException
{
    private const string Error = "Player with this login info does not exist";
    
    public PlayerNotExistsException():base(Error)
    {
        
    }
}