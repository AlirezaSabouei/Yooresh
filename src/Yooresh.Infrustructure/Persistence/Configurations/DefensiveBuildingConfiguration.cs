using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class DefensiveBuildingConfiguration : IEntityTypeConfiguration<DefensiveBuilding>
{
    public void Configure(EntityTypeBuilder<DefensiveBuilding> builder)
    {
        builder.ToTable(nameof(DefensiveBuilding));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(a => a.DefensiveBuildingType)
            .HasColumnType("nvarchar(20)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (DefensiveBuildingType)Enum.Parse(typeof(DefensiveBuildingType), v)
            )
            .HasMaxLength(20);

        builder.Property(a => a.Level)
            .IsRequired();

        builder.Property(a => a.IsUnderUpgrade)
            .IsRequired();

        builder.Property(a => a.HitPoint)
            .IsRequired();

        builder.OwnsOne(a => a.Player, ConfigurePlayer);

        builder.OwnsOne(a => a.Defence, ConfigureDefense);

        builder.OwnsOne(a => a.Attack, ConfigureAttack);

        builder.OwnsOne(a => a.RepairCost, ConfigureRepairCost);
    }

    private void ConfigurePlayer(OwnedNavigationBuilder<DefensiveBuilding, PlayerReference> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Id)
            .HasColumnName(nameof(DefensiveBuilding.Player) + nameof(PlayerReference.Id))
            .IsRequired();
    }

    private void ConfigureDefense(OwnedNavigationBuilder<DefensiveBuilding, Defense> ownedNavigationBuilder)
    {
        ownedNavigationBuilder.Property(a => a.AgainstMeleeInfantry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstRangeInfantry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstCavalry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstMage).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstFlyingCavalry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstSiegeUnit).IsRequired();
    }

    private void ConfigureAttack(OwnedNavigationBuilder<DefensiveBuilding, Attack> ownedNavigationBuilder)
    {
        ownedNavigationBuilder.Property(a => a.AgainstMeleeInfantry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstRangeInfantry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstCavalry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstMage).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstFlyingCavalry).IsRequired();
        ownedNavigationBuilder.Property(a => a.AgainstSiegeUnit).IsRequired();
    }

    private void ConfigureRepairCost(OwnedNavigationBuilder<DefensiveBuilding, ResourceValueObject> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(DefensiveBuilding.RepairCost) + nameof(ResourceValueObject.Gold))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(DefensiveBuilding.RepairCost) + nameof(ResourceValueObject.Lumber))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(DefensiveBuilding.RepairCost) + nameof(ResourceValueObject.Stone))
            .IsRequired();

        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(DefensiveBuilding.RepairCost) + nameof(ResourceValueObject.Food))
            .IsRequired();
    }
}
