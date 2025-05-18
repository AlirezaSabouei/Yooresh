using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Villages.Entities;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class VillageBuildingConfiguration : IEntityTypeConfiguration<VillageBuilding>
{
    public void Configure(EntityTypeBuilder<VillageBuilding> builder)
    {
        builder.ToTable(nameof(VillageBuilding));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        ConfigureAutoIncludes(builder);
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<VillageBuilding> builder)
    {
        builder.Navigation(a => a.Building).AutoInclude();
        builder.Navigation(a => a.Village).AutoInclude();
    }
}