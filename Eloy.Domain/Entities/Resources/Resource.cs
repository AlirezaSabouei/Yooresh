namespace Eloy.Domain.Entities.Resources;

public abstract class Resource
{
    public abstract string Name { get; }
    public abstract ResourceType ResourceType { get; }
}