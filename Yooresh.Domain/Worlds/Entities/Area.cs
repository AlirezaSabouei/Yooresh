using Yooresh.Domain.Common.Entities;

namespace Yooresh.Domain.Worlds.Entities;

public class Area : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid RegionId { get; set; }
    public Region Region { get; set; } = null!;
}
