using Yooresh.Domain.Common;

namespace Yooresh.Domain.Entities.World
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
