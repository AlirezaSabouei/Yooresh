using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.ResourceBuildings.Entities;

namespace Villages.Infrastructure.Persistence.Configurations;

public class LumbermillConfiguration : IEntityTypeConfiguration<LumberMill>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";
    
    public void Configure(EntityTypeBuilder<LumberMill> builder)
    {
        builder.ToTable(nameof(LumberMill));

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        ConfigureAutoIncludes(builder);
        
        SeedLumbermills(builder);
    }
    private void ConfigureHourlyProduction(OwnedNavigationBuilder<LumberMill, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(LumberMill.HourlyProduction) + nameof(Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(LumberMill.HourlyProduction) + nameof(Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(LumberMill.HourlyProduction) + nameof(Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(LumberMill.HourlyProduction) + nameof(Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(LumberMill.HourlyProduction) + nameof(Resource.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<LumberMill> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }
    
    private void SeedLumbermills(EntityTypeBuilder<LumberMill> builder)
    {
        var ids = File
            .ReadLines($"{basePath}LumberMill.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedResourceBuilding(new LumberMill()
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

    private void SeedResourceBuilding(LumberMill lumbermill, EntityTypeBuilder<LumberMill> builder)
    {
        builder.HasData(
            new
            {
                lumbermill.Id,
                lumbermill.Name,
                lumbermill.UpgradeDuration,
                lumbermill.NeedBuilderForUpgrade,
                lumbermill.UpgradeName,
                lumbermill.TargetId,
                lumbermill.Level,
                lumbermill.BuildingType
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                lumbermill.HourlyProduction.Food,
                lumbermill.HourlyProduction.Lumber,
                lumbermill.HourlyProduction.Stone,
                lumbermill.HourlyProduction.Gold,
                lumbermill.HourlyProduction.Metal,
                LumberMillId = lumbermill.Id,
                lumbermill.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                lumbermill.UpgradeCost.Food,
                lumbermill.UpgradeCost.Lumber,
                lumbermill.UpgradeCost.Stone,
                lumbermill.UpgradeCost.Gold,
                lumbermill.UpgradeCost.Metal,
                BuildingId = lumbermill.Id,
                lumbermill.Level
            });
    }
}