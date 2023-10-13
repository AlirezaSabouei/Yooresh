using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Enums;
using Yooresh.Domain.ValueObjects;

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

        builder.HasOne(a => a.Target)
            .WithMany()
            .HasForeignKey(a => a.TargetId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(a => a.ProductionType)
            .HasColumnType("nvarchar(10)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (ResourceType)Enum.Parse(typeof(ResourceType), v) // Parse string to enum                
            )
            .HasMaxLength(20);


        builder.Property(a => a.UpgradeName)
            .HasMaxLength(100);

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        SeedFarms(builder);
        SeedLumberMills(builder);
        SeedStoneMines(builder);
        SeedMetalMines(builder);

        ConfigureAutoIncludes(builder);
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

    private void SeedFarms(EntityTypeBuilder<ResourceBuilding> builder)
    {
        var ids = new List<Guid>();
        for (int i = 0; i <= 25; i++)
        {
            ids.Add(Guid.NewGuid());
        }

        for (int i = 25; i >= 0; i--)
        {
            SeedResourceBuilding(new ResourceBuilding()
            {
                Id = ids[i],
                Name = i == 0 ? $"Damaged Farm" : $"Farm {i}",
                ProductionType = ResourceType.Food,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                HourlyProduction = new Resource(i * i * 60, 0, 0, 0, 0),
                UpgradeCost = i == 1 ? new Resource(0, 0, 0, 0, 0) : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                NeedBuilderForUpgrade = true,
                UpgradeName = i == 0 ? $"Repair The Farm" : $"Upgrade To Farm {i + 1}",
                TargetId = i == 25 ? null : ids[i + 1],
                Level = i,
                LastResourceGatherDate = DateTimeOffset.UtcNow
            }, builder);
        }
    }

    private void SeedLumberMills(EntityTypeBuilder<ResourceBuilding> builder)
    {
        var ids = new List<Guid>();
        for (int i = 0; i <= 25; i++)
        {
            ids.Add(Guid.NewGuid());
        }

        for (int i = 25; i >= 0; i--)
        {
            SeedResourceBuilding(new ResourceBuilding()
            {
                Id = ids[i],
                Name = i == 0 ? $"Damaged Lumber Mill" : $"Lumber Mill {i}",
                ProductionType = ResourceType.Lumber,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                HourlyProduction = new Resource(0, i * i * 60, 0, 0, 0),
                UpgradeCost = i == 1 ? new Resource(0, 0, 0, 0, 0) : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                NeedBuilderForUpgrade = true,
                UpgradeName = i == 0 ? $"Repair The Lumber Mill" : $"Upgrade To Lumber Mill {i + 1}",
                TargetId = i == 25 ? null : ids[i + 1],
                Level = i,
                LastResourceGatherDate = DateTimeOffset.UtcNow
            }, builder);
        }
    }
    private void SeedStoneMines(EntityTypeBuilder<ResourceBuilding> builder)
    {
        var ids = new List<Guid>();
        for (int i = 0; i <= 25; i++)
        {
            ids.Add(Guid.NewGuid());
        }

        for (int i = 25; i >= 0; i--)
        {
            SeedResourceBuilding(new ResourceBuilding()
            {
                Id = ids[i],
                Name = i == 0 ? $"Damaged Stone Mine" : $"Stone Mine {i}",
                ProductionType = ResourceType.Stone,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                HourlyProduction = new Resource(0, 0, i * i * 60, 0, 0),
                UpgradeCost = i == 1 ? new Resource(0, 0, 0, 0, 0) : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                NeedBuilderForUpgrade = true,
                UpgradeName = i == 0 ? $"Repair The Stone Mine" : $"Upgrade To Stone Mine {i + 1}",
                TargetId = i == 25 ? null : ids[i + 1],
                Level = i,
                LastResourceGatherDate = DateTimeOffset.UtcNow
            }, builder);
        }
    }
    private void SeedMetalMines(EntityTypeBuilder<ResourceBuilding> builder)
    {
        var ids = new List<Guid>();
        for (int i = 0; i <= 25; i++)
        {
            ids.Add(Guid.NewGuid());
        }

        for (int i = 25; i >= 0; i--)
        {
            SeedResourceBuilding(new ResourceBuilding()
            {
                Id = ids[i],
                Name = i == 0 ? $"Damaged Metal Mine" : $"Metal Mine {i}",
                ProductionType = ResourceType.Metal,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                HourlyProduction = new Resource(0, 0, 0, 0, i * i * 60),
                UpgradeCost = i == 1 ? new Resource(0, 0, 0, 0, 0) : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                NeedBuilderForUpgrade = true,
                UpgradeName = i == 0 ? $"Repair The Metal Mine" : $"Upgrade To Metal Mine {i + 1}",
                TargetId = i == 25 ? null : ids[i + 1],
                Level = i,
                LastResourceGatherDate = DateTimeOffset.UtcNow
            }, builder);
        }
    }

    private void SeedResourceBuilding(ResourceBuilding resourceBuilding, EntityTypeBuilder<ResourceBuilding> builder)
    {
        builder.HasData(
            new
            {
                resourceBuilding.Id,
                resourceBuilding.Name,
                resourceBuilding.ProductionType,
                resourceBuilding.UpgradeDuration,
                resourceBuilding.NeedBuilderForUpgrade,
                resourceBuilding.UpgradeName,
                resourceBuilding.TargetId,
                resourceBuilding.Level,
                resourceBuilding.LastResourceGatherDate
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                resourceBuilding.HourlyProduction.Food,
                resourceBuilding.HourlyProduction.Lumber,
                resourceBuilding.HourlyProduction.Stone,
                resourceBuilding.HourlyProduction.Gold,
                resourceBuilding.HourlyProduction.Metal,
                ResourceBuildingId = resourceBuilding.Id,
                resourceBuilding.Level,
                resourceBuilding.LastResourceGatherDate
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                resourceBuilding.UpgradeCost.Food,
                resourceBuilding.UpgradeCost.Lumber,
                resourceBuilding.UpgradeCost.Stone,
                resourceBuilding.UpgradeCost.Gold,
                resourceBuilding.UpgradeCost.Metal,
                ResourceBuildingId = resourceBuilding.Id,
                resourceBuilding.Level,
                resourceBuilding.LastResourceGatherDate
            });
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<ResourceBuilding> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }

}