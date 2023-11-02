using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Enums;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class TowerConfiguration : IEntityTypeConfiguration<Tower>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<Tower> builder)
    {
        builder.ToTable(nameof(Tower));

        builder.Property(a => a.Health)
            .IsRequired();
        
        builder.Property(a => a.TroopCapacity)
            .IsRequired();
        
        builder.OwnsOne(a => a.Defense, ConfigureDefense);
        
        builder.OwnsOne(a => a.RepairCost, ConfigureRepairCost);
        
        ConfigureAutoIncludes(builder);

        SeedTowers(builder);
    }

    private void ConfigureDefense(OwnedNavigationBuilder<Tower, Defense> ownedNavigationBuilder)
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

    private void ConfigureRepairCost(OwnedNavigationBuilder<Tower, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Tower.RepairCost) + nameof(Resource.Gold))
            .IsRequired();
    }
    private void SeedTowers(EntityTypeBuilder<Tower> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Tower.txt")
            .Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedTower(new Tower()
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
                Level = i,
                TroopCapacity = i * i * 10
            }, builder);
        }
    }

    private void SeedTower(Tower tower, EntityTypeBuilder<Tower> builder)
    {
        builder.HasData(
            new
            {
                tower.Id,
                tower.Health,
                tower.UpgradeDuration,
                tower.TargetId,
                tower.Level,
                tower.TroopCapacity,
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
                TowerId = tower.Id,
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
                TowerId = tower.Id,
                tower.Level
            });
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<Tower> builder)
    {
        builder.Navigation(a => a.RepairCost).AutoInclude();
        builder.Navigation(a => a.Defense).AutoInclude();
    }
}
