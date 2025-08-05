using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class WallConfiguration : IEntityTypeConfiguration<Wall>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<Wall> builder)
    {
        builder.ToTable(nameof(Wall));

        builder.OwnsOne(a => a.Defense, ConfigureDefense);
        
        builder.OwnsOne(a => a.RepairCost, ConfigureRepairCost);
        
        ConfigureAutoIncludes(builder);

        SeedWalls(builder);
    }

    private void ConfigureDefense(OwnedNavigationBuilder<Wall, Defense> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.AgainstMeleeInfantry)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstMeleeInfantry))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.AgainstRangeInfantry)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstRangeInfantry))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.AgainstCavalry)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstCavalry))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.AgainstMage)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstMage))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.AgainstFlyingCavalry)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstFlyingCavalry))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.AgainstSiegeUnit)
            .HasColumnName(nameof(Defense) + nameof(Defense.AgainstSiegeUnit))
            .IsRequired();
    }

    private void ConfigureRepairCost(OwnedNavigationBuilder<Wall, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(ResourceValueObject.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(ResourceValueObject.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();
    }
    
    private void ConfigureAutoIncludes(EntityTypeBuilder<Wall> builder)
    {
        builder.Navigation(a => a.RepairCost).AutoInclude();
        builder.Navigation(a => a.Defense).AutoInclude();
    }

    private void SeedWalls(EntityTypeBuilder<Wall> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Wall.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedWall(new Wall()
            {
                Id = ids[i],
                Defense = new Defense(i * i * 5, i * i * 5, i * i * 5, i * i * 5, i * i * 5, i * i * 1),
                Health = i * i * 25,
                RepairCost = new Resource(i * i * 60, 0, 0, 0, 0),
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                UpgradeCost = i == 1
                    ? new Resource(0, 0, 0, 0, 0)
                    : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                TargetId = i == 24 ? null : ids[i + 1],
                Level = i
            }, builder);
        }
    }

    private void SeedWall(Wall tower, EntityTypeBuilder<Wall> builder)
    {
        builder.HasData(
            new
            {
                tower.Id,
                tower.Health,
                tower.UpgradeDuration,
                tower.NeedBuilderForUpgrade,
                tower.TargetId,
                tower.Level,
                tower.BuildingType
            });

        builder.OwnsOne(a => a.RepairCost).HasData(
            new
            {
                tower.RepairCost.Food,
                tower.RepairCost.Lumber,
                tower.RepairCost.Stone,
                tower.RepairCost.Gold,
                tower.RepairCost.Metal,
                WallId = tower.Id,
                tower.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                tower.UpgradeCost.Food,
                tower.UpgradeCost.Lumber,
                tower.UpgradeCost.Stone,
                tower.UpgradeCost.Gold,
                tower.UpgradeCost.Metal,
                BuildingId = tower.Id,
                tower.Level
            });

        builder.OwnsOne(a => a.Defense).HasData(
            new
            {
                tower.Defense.AgainstMeleeInfantry,
                tower.Defense.AgainstRangeInfantry,
                tower.Defense.AgainstCavalry,
                tower.Defense.AgainstMage,
                tower.Defense.AgainstFlyingCavalry,
                tower.Defense.AgainstSiegeUnit,
                WallId = tower.Id,
                tower.Level
            });
    }
}