using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class ResourceBuildingUpgradeConfiguration : IEntityTypeConfiguration<ResourceBuildingUpgrade>
{
    private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        builder.ToTable(nameof(ResourceBuildingUpgrade));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Level)
            .IsRequired();

        builder.Property(a => a.ResourceBuildingType)
            .HasColumnType("nvarchar(20)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (ResourceBuildingType)Enum.Parse(typeof(ResourceBuildingType), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.Property(a => a.UpgradeName)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsOne(a => a.UpgradeCost, ConfigureUpgradeCost);

        builder.Property(a => a.BonusHarvestRatePerMinute)
            .IsRequired();

        builder.Property(a => a.UpgradeDuration)
            .IsRequired();

        builder.Property(a => a.NeedBuilderForUpgrade)
            .IsRequired();

        ConfigureAutoIncludes(builder);
        SeedGoldMines(builder);
        SeedLumberMills(builder);
        SeedStoneMines(builder);
        SeedFarms(builder);
    }
    private void ConfigureUpgradeCost(OwnedNavigationBuilder<ResourceBuildingUpgrade, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(ResourceBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(ResourceBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(ResourceBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(ResourceBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Food))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        builder.Navigation(a => a.UpgradeCost).AutoInclude();
    }

    private void SeedGoldMines(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}GoldMine.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedResourceBuildingUpgrade(new ResourceBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                ResourceBuildingType = ResourceBuildingType.GoldMine,
                UpgradeName = $"Gold mine level {i+1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusHarvestRatePerMinute = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }

    private void SeedResourceBuildingUpgrade(ResourceBuildingUpgrade resourceBuildingUpgrade, EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        builder.HasData(
            new
            {
                resourceBuildingUpgrade.Id,
                resourceBuildingUpgrade.Level,
                resourceBuildingUpgrade.ResourceBuildingType,
                resourceBuildingUpgrade.UpgradeName,
                // goldMine.UpgradeCost,
                resourceBuildingUpgrade.BonusHarvestRatePerMinute,
                resourceBuildingUpgrade.UpgradeDuration,
                resourceBuildingUpgrade.NeedBuilderForUpgrade
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                resourceBuildingUpgrade.UpgradeCost.Food,
                resourceBuildingUpgrade.UpgradeCost.Lumber,
                resourceBuildingUpgrade.UpgradeCost.Stone,
                resourceBuildingUpgrade.UpgradeCost.Gold,
                BuildingId = resourceBuildingUpgrade.Id,
                resourceBuildingUpgrade.Level
            });
    }

    private void SeedLumberMills(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}LumberMill.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedResourceBuildingUpgrade(new ResourceBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                ResourceBuildingType = ResourceBuildingType.LumberMill,
                UpgradeName = $"Lumber mill level {i+1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusHarvestRatePerMinute = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }

    private void SeedStoneMines(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}StoneMine.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedResourceBuildingUpgrade(new ResourceBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                ResourceBuildingType = ResourceBuildingType.StoneMine,
                UpgradeName = $"Stone mine level {i + 1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusHarvestRatePerMinute = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }

    private void SeedFarms(EntityTypeBuilder<ResourceBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Farm.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedResourceBuildingUpgrade(new ResourceBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                ResourceBuildingType = ResourceBuildingType.Farm,
                UpgradeName = $"Stone mine level {i + 1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusHarvestRatePerMinute = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }
}
