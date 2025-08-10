using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class DefensiveBuildingUpgradeConfiguration : IEntityTypeConfiguration<DefensiveBuildingUpgrade>
{
    private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<DefensiveBuildingUpgrade> builder)
    {
        builder.ToTable(nameof(DefensiveBuildingUpgrade));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Level)
            .IsRequired();

        builder.Property(a => a.DefensiveBuildingType)
            .HasColumnType("nvarchar(20)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (DefensiveBuildingType)Enum.Parse(typeof(DefensiveBuildingType), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.Property(a => a.UpgradeName)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsOne(a => a.UpgradeCost, ConfigureUpgradeCost);

        builder.OwnsOne(a => a.RepairCost, ConfigureRepairCost);

        builder.Property(a => a.BonusAttack)
            .IsRequired();

        builder.Property(a => a.BonusDefense)
            .IsRequired();

        builder.Property(a => a.UpgradeDuration)
            .IsRequired();

        builder.Property(a => a.NeedBuilderForUpgrade)
            .IsRequired();

        ConfigureAutoIncludes(builder);
        SeedTowers(builder);
        SeedWalls(builder);
    }

    private void ConfigureUpgradeCost(OwnedNavigationBuilder<DefensiveBuildingUpgrade, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.UpgradeCost) + nameof(ResourceValueObject.Food))
            .IsRequired();
    }

    private void ConfigureRepairCost(OwnedNavigationBuilder<DefensiveBuildingUpgrade, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.RepairCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.RepairCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.RepairCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(DefensiveBuildingUpgrade.RepairCost) + nameof(ResourceValueObject.Food))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<DefensiveBuildingUpgrade> builder)
    {
        builder.Navigation(a => a.UpgradeCost).AutoInclude();
    }

    private void SeedTowers(EntityTypeBuilder<DefensiveBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Tower.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedDefenseBuildingUpgrade(new DefensiveBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                DefensiveBuildingType = DefensiveBuildingType.Tower,
                UpgradeName = $"Tower level {i + 1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusAttack = i * 5,
                BonusDefense = i * 5,
                BonusHitPoint = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }

    private void SeedDefenseBuildingUpgrade(DefensiveBuildingUpgrade defensiveBuildingUpgrade, EntityTypeBuilder<DefensiveBuildingUpgrade> builder)
    {
        builder.HasData(
            new
            {
                defensiveBuildingUpgrade.Id,
                defensiveBuildingUpgrade.Level,
                defensiveBuildingUpgrade.DefensiveBuildingType,
                defensiveBuildingUpgrade.UpgradeName,
                defensiveBuildingUpgrade.BonusAttack,
                defensiveBuildingUpgrade.BonusDefense,
                defensiveBuildingUpgrade.BonusHitPoint,
                defensiveBuildingUpgrade.UpgradeDuration,
                defensiveBuildingUpgrade.NeedBuilderForUpgrade
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                defensiveBuildingUpgrade.UpgradeCost.Food,
                defensiveBuildingUpgrade.UpgradeCost.Lumber,
                defensiveBuildingUpgrade.UpgradeCost.Stone,
                defensiveBuildingUpgrade.UpgradeCost.Gold,
                BuildingId = defensiveBuildingUpgrade.Id,
                defensiveBuildingUpgrade.Level
            });
    }

    private void SeedWalls(EntityTypeBuilder<DefensiveBuildingUpgrade> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Wall.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 20; i >= 0; i--)
        {
            SeedDefenseBuildingUpgrade(new DefensiveBuildingUpgrade()
            {
                Id = ids[i],
                Level = i,
                DefensiveBuildingType = DefensiveBuildingType.Wall,
                UpgradeName = $"Wall level {i + 1}",
                UpgradeCost = i == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                BonusAttack = i * 5,
                BonusDefense = i * 5,
                BonusHitPoint = i * 5,
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                NeedBuilderForUpgrade = true
            }, builder);
        }
    }
}