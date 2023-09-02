namespace Eloy.Domain.Entities.Resources.Extends;

public class Crop : Resource
{
    public override string Name => nameof(Crop);
    public override ResourceType ResourceType => ResourceType.Crop;
}