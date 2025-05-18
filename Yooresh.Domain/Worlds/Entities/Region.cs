using Yooresh.Domain.Common.Entities;

namespace Yooresh.Domain.Worlds.Entities
{
    public class Region : BaseEntity
    {
        public Region()
        {
            Areas = new List<Area>();
        }
        public string Name { get; set; } = null!;
        public string Story { get; set; } = null!;
        public List<Area> Areas { get; set; }
    }
}
