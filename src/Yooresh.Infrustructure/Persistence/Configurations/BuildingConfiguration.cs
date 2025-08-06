using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable(nameof(Building));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        
        builder.Property(a => a.BuildingType)
            .HasColumnType("nvarchar(30)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (BuildingType)Enum.Parse(typeof(BuildingType), v) // Parse string to enum                
            )
            .HasMaxLength(20);
        
        builder.Property(a => a.UpgradeDuration)
            .IsRequired();
        
        builder.HasOne(a => a.Target)
            .WithMany()
            .HasForeignKey(a => a.TargetId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.OwnsOne(a => a.UpgradeCost, ConfigureUpgradeCost);
        
        ConfigureAutoIncludes(builder);
    }
    private void ConfigureUpgradeCost(OwnedNavigationBuilder<Building, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Building.UpgradeCost) + nameof(ResourceValueObject.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Building.UpgradeCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Building.UpgradeCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Building.UpgradeCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<Building> builder)
    {
        builder.Navigation(a => a.UpgradeCost).AutoInclude();
        builder.Navigation(a => a.Target).AutoInclude();
    }
}
