namespace Yooresh.Domain.Entities.Villages;

public class PlayerReference : BaseEntity
{
    private PlayerReference()
    {
        
    }

    public PlayerReference(Guid id)
    {
        Id = id;
    }
}