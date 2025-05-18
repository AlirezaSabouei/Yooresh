using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.ResourceBuildings.Entities;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class FarmConfiguration : IEntityTypeConfiguration<Farm>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<Farm> builder)
    {
        builder.ToTable(nameof(Farm));

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        ConfigureAutoIncludes(builder);
        
        SeedFarms(builder);
    }

    private void ConfigureHourlyProduction(OwnedNavigationBuilder<Farm, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Farm.HourlyProduction) + nameof(Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Farm.HourlyProduction) + nameof(Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(Farm.HourlyProduction) + nameof(Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Farm.HourlyProduction) + nameof(Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Farm.HourlyProduction) + nameof(Resource.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<Farm> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }

    private void SeedFarms(EntityTypeBuilder<Farm> builder)
    {
        var ids = File
            .ReadLines($"{basePath}Farm.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedResourceBuilding(new Farm()
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

    private void SeedResourceBuilding(Farm farm, EntityTypeBuilder<Farm> builder)
    {
        builder.HasData(
            new
            {
                farm.Id,
                farm.Name,
                farm.UpgradeDuration,
                farm.NeedBuilderForUpgrade,
                farm.UpgradeName,
                farm.TargetId,
                farm.Level,
                farm.BuildingType
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                farm.HourlyProduction.Food,
                farm.HourlyProduction.Lumber,
                farm.HourlyProduction.Stone,
                farm.HourlyProduction.Gold,
                farm.HourlyProduction.Metal,
                FarmId = farm.Id,
                farm.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                farm.UpgradeCost.Food,
                farm.UpgradeCost.Lumber,
                farm.UpgradeCost.Stone,
                farm.UpgradeCost.Gold,
                farm.UpgradeCost.Metal,
                BuildingId = farm.Id,
                farm.Level
            });
    }
}