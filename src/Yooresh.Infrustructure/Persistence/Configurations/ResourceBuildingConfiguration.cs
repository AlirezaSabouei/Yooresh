using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.ResourceBuildings;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class ResourceBuildingConfiguration : IEntityTypeConfiguration<ResourceBuilding>
{
    public void Configure(EntityTypeBuilder<ResourceBuilding> builder)
    {
        builder.ToTable(nameof(ResourceBuilding));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(a => a.ResourceBuildingType)
            .HasColumnType("nvarchar(20)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (ResourceBuildingType)Enum.Parse(typeof(ResourceBuildingType), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.Property(a => a.Level)
            .IsRequired();

        builder.Property(a => a.HarvestRatePerMinute)
            .IsRequired();

        builder.OwnsOne(a => a.Player, ConfigurePlayer);
    }
    private void ConfigurePlayer(OwnedNavigationBuilder<ResourceBuilding, PlayerReference> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Id)
            .HasColumnName(nameof(Player) + nameof(PlayerReference.Id))
            .IsRequired();
    }
}
