using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Account;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(nameof(Resource));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.ResourceType)
            .HasColumnType("nvarchar(24)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(),         // Convert enum to string
                v => (ResourceType)Enum.Parse(typeof(ResourceType), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.OwnsOne(a => a.Player, ConfigureResource);
        ConfigureAutoIncludes(builder);
    }

    private void ConfigureResource(OwnedNavigationBuilder<Resource, PlayerReference> ownedNavigationBuilder)
    {
        ownedNavigationBuilder
            .Property(a => a.Id)
            .HasColumnName(nameof(Resource.Player) + nameof(Resource.Player.Id))
            .IsRequired();
    }

    private void ConfigureAutoIncludes(EntityTypeBuilder<Resource> builder)
    {
        builder.Navigation(a => a.Player).AutoInclude();
    }
}