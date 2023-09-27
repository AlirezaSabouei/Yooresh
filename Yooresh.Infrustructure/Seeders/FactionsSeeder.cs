using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yooresh.Infrastructure.Seeders;

/*public class FactionsSeeder : IEntityTypeConfiguration<Faction>
{
    public void Configure(EntityTypeBuilder<Faction> builder)
    {
        /*builder.HasData(
            new Faction
            {
                Name = "Human",
                FactionProperties = new FactionProperties()
                {
                    Advantages =
                    {
                        "Fast development",
                        "Better defensive buildings",
                        "Cheap units",
                        "Strong Cavalry"
                    },
                    Disadvantages =
                    {
                        "Weak foot Soldiers"
                    }
                }
            }
        );#1#
    }
}*/