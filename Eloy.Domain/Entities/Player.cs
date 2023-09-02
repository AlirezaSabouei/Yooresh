using Eloy.Domain.Entities.ResourceBuildings;

namespace Eloy.Domain.Entities;

public class Player:BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Confirmed { get; set; }
    public string VillageName { get; set; }
    public ICollection<ResourceBuilding>? ResourceBuildings { get; set; }
    
}
