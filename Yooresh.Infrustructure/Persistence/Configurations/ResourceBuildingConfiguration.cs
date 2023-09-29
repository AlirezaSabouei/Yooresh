using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class ResourceBuildingConfiguration : IEntityTypeConfiguration<ResourceBuilding>
{
    public void Configure(EntityTypeBuilder<ResourceBuilding> builder)
    {
        builder.ToTable(nameof(ResourceBuilding));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(a => a.UpgradeCost, ConfigureUpgradeCost);

        builder.Property(a => a.UpgradeDuration)
            .IsRequired();

        builder.HasOne(a => a.Requirement)
            .WithMany()
            .HasForeignKey(a => a.RequirementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.ProductionType)
            .HasColumnType("nvarchar(10)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (ProductionType)Enum.Parse(typeof(ProductionType), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        SeedFarmsData(builder);
    }

    private void ConfigureUpgradeCost(OwnedNavigationBuilder<ResourceBuilding, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(ResourceBuilding.UpgradeCost) + nameof(Village.Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(ResourceBuilding.UpgradeCost) + nameof(Village.Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(ResourceBuilding.UpgradeCost) + nameof(Village.Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(ResourceBuilding.UpgradeCost) + nameof(Village.Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(ResourceBuilding.UpgradeCost) + nameof(Village.Resource.Gold))
            .IsRequired();
    }

    private void ConfigureHourlyProduction(OwnedNavigationBuilder<ResourceBuilding, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(ResourceBuilding.HourlyProduction) + nameof(Village.Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(ResourceBuilding.HourlyProduction) + nameof(Village.Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(ResourceBuilding.HourlyProduction) + nameof(Village.Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(ResourceBuilding.HourlyProduction) + nameof(Village.Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(ResourceBuilding.HourlyProduction) + nameof(Village.Resource.Gold))
            .IsRequired();
    }

    private void SeedFarmsData(EntityTypeBuilder<ResourceBuilding> builder)
    {
        SeedFarm(new ResourceBuilding(
            "Farm1",
            new Resource(600, 0, 0, 0, 0),
            new Resource(0, 0, 0, 0, 0),
            ProductionType.Food,
            new TimeSpan(0, 1, 0),
            new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0")
        ), builder);

        SeedFarm(new ResourceBuilding(
            "Farm2",
            new Resource(1200, 0, 0, 0, 0),
            new Resource(10, 10, 0, 0, 0),
            ProductionType.Food,
            new TimeSpan(0, 2, 0),
            new Guid("0986a9a2-d235-4a62-8ae4-1eb1a31eab18"),
            new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0")
        ), builder);
    }

    private void SeedFarm(ResourceBuilding resourceBuilding, EntityTypeBuilder<ResourceBuilding> builder)
    {
        builder.HasData(
            new
            {
                Id = resourceBuilding.Id,
                Name = resourceBuilding.Name,
                ProductionType = resourceBuilding.ProductionType,
                UpgradeDuration = resourceBuilding.UpgradeDuration,
                RequirementId = resourceBuilding.RequirementId
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
                new
                {
                    Food = resourceBuilding.HourlyProduction.Food,
                    Lumber = resourceBuilding.HourlyProduction.Lumber,
                    Stone = resourceBuilding.HourlyProduction.Stone,
                    Gold = resourceBuilding.HourlyProduction.Gold,
                    Metal = resourceBuilding.HourlyProduction.Metal,
                    ResourceBuildingId = resourceBuilding.Id
                });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
                new
                {
                    resourceBuilding.UpgradeCost.Food,
                    resourceBuilding.UpgradeCost.Lumber,
                    resourceBuilding.UpgradeCost.Stone,
                    resourceBuilding.UpgradeCost.Gold,
                    resourceBuilding.UpgradeCost.Metal,
                    ResourceBuildingId = resourceBuilding.Id
                });
    }
}