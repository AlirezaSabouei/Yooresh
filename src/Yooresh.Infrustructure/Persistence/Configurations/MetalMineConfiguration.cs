using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Application.Villages.Dto;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class MetalMineConfiguration : IEntityTypeConfiguration<MetalMine>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<MetalMine> builder)
    {
        builder.ToTable(nameof(MetalMine));

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        ConfigureAutoIncludes(builder);
        
        SeedMetalMines(builder);
    }
    private void ConfigureHourlyProduction(OwnedNavigationBuilder<MetalMine, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(MetalMine.HourlyProduction) + nameof(ResourceValueObject.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(MetalMine.HourlyProduction) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(MetalMine.HourlyProduction) + nameof(ResourceValueObject.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(MetalMine.HourlyProduction) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(MetalMine.HourlyProduction) + nameof(ResourceValueObject.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<MetalMine> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }
    
    private void SeedMetalMines(EntityTypeBuilder<MetalMine> builder)
    {
        var ids = File
            .ReadLines($"{basePath}MetalMine.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedResourceBuilding(new MetalMine()
            {
                Id = ids[i],
                UpgradeDuration = new TimeSpan(0, i * i * i, 0),
                HourlyProduction = new Resource(i * i * 60, 0, 0, 0, 0),
                UpgradeCost = i == 1
                    ? new Resource(0, 0, 0, 0, 0)
                    : new Resource(i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10, i ^ 2 * 10),
                TargetId = i == 24 ? null : ids[i + 1],
                Level = i
            }, builder);
        }
    }

    private void SeedResourceBuilding(MetalMine metalMine, EntityTypeBuilder<MetalMine> builder)
    {
        builder.HasData(
            new
            {
                metalMine.Id,
                metalMine.Name,
                metalMine.UpgradeDuration,
                metalMine.NeedBuilderForUpgrade,
                metalMine.UpgradeName,
                metalMine.TargetId,
                metalMine.Level,
                metalMine.BuildingType
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                metalMine.HourlyProduction.Food,
                metalMine.HourlyProduction.Lumber,
                metalMine.HourlyProduction.Stone,
                metalMine.HourlyProduction.Gold,
                metalMine.HourlyProduction.Metal,
                MetalMineId = metalMine.Id,
                metalMine.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                metalMine.UpgradeCost.Food,
                metalMine.UpgradeCost.Lumber,
                metalMine.UpgradeCost.Stone,
                metalMine.UpgradeCost.Gold,
                metalMine.UpgradeCost.Metal,
                BuildingId = metalMine.Id,
                metalMine.Level
            });
    }
}