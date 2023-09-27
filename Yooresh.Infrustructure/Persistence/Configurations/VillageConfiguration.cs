using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class VillageConfiguration : IEntityTypeConfiguration<Village>
{
    public void Configure(EntityTypeBuilder<Village> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(a => a.Faction)
            .WithMany()
            .IsRequired();
        
       // builder.OwnsOne(a => a.Faction.Name, b =>
       // {
           // b.Property(fi => fi.SomeProperty)
              //  .HasColumnName(nameof(FactionInfo.SomeProperty))
               // .IsRequired();

            // Add more configuration for FactionInfo properties here if needed
       // });

        builder.OwnsOne(a => a.Player, b => ConfigurePersonReference(b));
        builder.OwnsOne(a => a.Resource, b => ConfigureVillageResource(b));

        builder.OwnsMany(a => a.Troops, b => b.WithOwner());
    }

    private void ConfigurePersonReference(OwnedNavigationBuilder<Village, PlayerReference> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Id)
            .HasColumnName(nameof(Village.Player) + nameof(Village.Player.Id))
            .IsRequired();
    }
    
    private void ConfigureVillageResource(OwnedNavigationBuilder<Village, Resource> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Food)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Food))
            .IsRequired();
        
        ownedNavigationBuilder
            .Property(a => a.Lumber)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Lumber))
            .IsRequired();
        
        ownedNavigationBuilder
            .Property(a => a.Metal)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Metal))
            .IsRequired();
        
        ownedNavigationBuilder
            .Property(a => a.Stone)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Stone))
            .IsRequired();
        
        ownedNavigationBuilder
            .Property(a => a.Gold)
            .HasColumnName(nameof(Village.Resource) + nameof(Village.Resource.Gold))
            .IsRequired();
    }
}