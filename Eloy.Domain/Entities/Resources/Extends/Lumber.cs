namespace Eloy.Domain.Entities.Resources.Extends;

public class Lumber : Resource
{
    public override string Name => nameof(Lumber);
    public override ResourceType ResourceType => ResourceType.Lumber;
}