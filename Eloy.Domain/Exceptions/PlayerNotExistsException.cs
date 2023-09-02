namespace Eloy.Domain.Exceptions;

public class PlayerNotExistsException : Exception
{
    private const string Error = "Player with this login info does not exist";
    
    public PlayerNotExistsException():base(Error)
    {
        
    }
}