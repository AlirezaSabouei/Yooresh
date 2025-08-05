using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class StoneMineConfiguration : IEntityTypeConfiguration<StoneMine>
{
    private string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Persistence\\Configurations\\Id\\";

    public void Configure(EntityTypeBuilder<StoneMine> builder)
    {
        builder.ToTable(nameof(StoneMine));

        builder.OwnsOne(a => a.HourlyProduction, ConfigureHourlyProduction);

        ConfigureAutoIncludes(builder);
        
        SeedStoneMines(builder);
    }
    private void ConfigureHourlyProduction(OwnedNavigationBuilder<StoneMine, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(StoneMine.HourlyProduction) + nameof(ResourceValueObject.Food))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(StoneMine.HourlyProduction) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(StoneMine.HourlyProduction) + nameof(ResourceValueObject.Metal))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(StoneMine.HourlyProduction) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(StoneMine.HourlyProduction) + nameof(ResourceValueObject.Gold))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<StoneMine> builder)
    {
        builder.Navigation(a => a.HourlyProduction).AutoInclude();
    }
    
    private void SeedStoneMines(EntityTypeBuilder<StoneMine> builder)
    {
        var ids = File
            .ReadLines($"{basePath}StoneMine.txt").Select(Guid.Parse)
            .ToList();

        for (int i = 24; i >= 0; i--)
        {
            SeedStoneMine(new StoneMine()
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

    private void SeedStoneMine(StoneMine stoneMine, EntityTypeBuilder<StoneMine> builder)
    {
        builder.HasData(
            new
            {
                stoneMine.Id,
                stoneMine.Name,
                stoneMine.UpgradeDuration,
                stoneMine.NeedBuilderForUpgrade,
                stoneMine.UpgradeName,
                stoneMine.TargetId,
                stoneMine.Level,
                stoneMine.BuildingType
            });

        builder.OwnsOne(a => a.HourlyProduction).HasData(
            new
            {
                stoneMine.HourlyProduction.Food,
                stoneMine.HourlyProduction.Lumber,
                stoneMine.HourlyProduction.Stone,
                stoneMine.HourlyProduction.Gold,
                stoneMine.HourlyProduction.Metal,
                StoneMineId = stoneMine.Id,
                stoneMine.Level
            });

        builder.OwnsOne(a => a.UpgradeCost).HasData(
            new
            {
                stoneMine.UpgradeCost.Food,
                stoneMine.UpgradeCost.Lumber,
                stoneMine.UpgradeCost.Stone,
                stoneMine.UpgradeCost.Gold,
                stoneMine.UpgradeCost.Metal,
                BuildingId = stoneMine.Id,
                stoneMine.Level
            });
    }
}