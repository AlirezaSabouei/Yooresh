using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Common.ValueObjects;
using Yooresh.Domain.ResourceBuildings.Entities;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class GoldMineConfiguration : IEntityTypeConfiguration<GoldMine>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<GoldMine> builder)
    {
        builder.ToTable(nameof(GoldMine));

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        ConfigureAutoIncludes(builder);
        
        SeedGoldMines(builder);
    }
    private void ConfigureHourlyProduction(OwnedNavigationBuilder<GoldMine, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(GoldMine.HourlyProduction) + nameof(Resource.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(GoldMine.HourlyProduction) + nameof(Resource.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(GoldMine.HourlyProduction) + nameof(Resource.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(GoldMine.HourlyProduction) + nameof(Resource.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(GoldMine.HourlyProduction) + nameof(Resource.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<GoldMine> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }
    
    private void SeedGoldMines(EntityTypeBuilder<GoldMine> builder)
    {
        var ids = File
            .ReadLines($"{basePath}GoldMine.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedGoldMine(new GoldMine()
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

    private void SeedGoldMine(GoldMine goldMine, EntityTypeBuilder<GoldMine> builder)
    {
        builder.HasData(
            new
            {
                goldMine.Id,
                goldMine.Name,
                goldMine.UpgradeDuration,
                goldMine.NeedBuilderForUpgrade,
                goldMine.UpgradeName,
                goldMine.TargetId,
                goldMine.Level,
                goldMine.BuildingType
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                goldMine.HourlyProduction.Food,
                goldMine.HourlyProduction.Lumber,
                goldMine.HourlyProduction.Stone,
                goldMine.HourlyProduction.Gold,
                goldMine.HourlyProduction.Metal,
                GoldMineId = goldMine.Id,
                goldMine.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                goldMine.UpgradeCost.Food,
                goldMine.UpgradeCost.Lumber,
                goldMine.UpgradeCost.Stone,
                goldMine.UpgradeCost.Gold,
                goldMine.UpgradeCost.Metal,
                BuildingId = goldMine.Id,
                goldMine.Level
            });
    }
}