namespace Yooresh.Domain.Entities.World;

public class Area : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid RegionId { get; set; }
    public Region Region { get; set; } = null!;
}
