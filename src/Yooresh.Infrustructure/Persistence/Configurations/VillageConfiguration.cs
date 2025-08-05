using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Village = Yooresh.Domain.Entities.Villages.Village;
using Yooresh.Domain.Entities.Troops;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class VillageConfiguration : IEntityTypeConfiguration<Village>
{
    public void Configure(EntityTypeBuilder<Village> builder)
    {
        builder.ToTable(nameof(Village));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(a => a.Player)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Faction)
            .WithMany()
            .HasForeignKey(a => a.FactionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(a => a.VillageResourceBuildings)
            .WithOne(b=>b.Village)
            .HasForeignKey(c=>c.VillageId);

        builder.OwnsOne(a => a.Resource, ConfigureResource);

        ConfigureAutoIncludes(builder);
    }

    private void ConfigureResource(OwnedNavigationBuilder<Village, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<Village> builder)
    {
        builder.Navigation(a => a.Faction).AutoInclude();
        builder.Navigation(a => a.Player).AutoInclude();
        builder.Navigation(a => a.VillageResourceBuildings).AutoInclude();
        builder.Navigation(a => a.Resource).AutoInclude();
    }
}
