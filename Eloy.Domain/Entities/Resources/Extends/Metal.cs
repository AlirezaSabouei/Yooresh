namespace Eloy.Domain.Entities.Resources.Extends;

public class Metal : Resource
{
    public override string Name => nameof(Lumber);
    public override ResourceType ResourceType => ResourceType.Lumber;
}