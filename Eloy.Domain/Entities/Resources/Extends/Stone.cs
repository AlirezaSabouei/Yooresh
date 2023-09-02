namespace Eloy.Domain.Entities.Resources.Extends;

public class Stone : Resource
{
    public override string Name => nameof(Stone);
    public override ResourceType ResourceType => ResourceType.Stone;
}