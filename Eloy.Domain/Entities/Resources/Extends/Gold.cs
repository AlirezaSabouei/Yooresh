namespace Eloy.Domain.Entities.Resources.Extends;

public class Gold : Resource
{
    public override string Name => nameof(Gold);
    public override ResourceType ResourceType => ResourceType.Gold;
}