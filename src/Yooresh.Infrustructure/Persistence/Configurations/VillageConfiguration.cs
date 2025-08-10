using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class VillageConfiguration : IEntityTypeConfiguration<Village>
{
    public void Configure(EntityTypeBuilder<Village> builder)
    {
        builder.ToTable(nameof(Village));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(a => a.Player)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Faction)
            .WithMany()
            .HasForeignKey(a => a.FactionId)
            .OnDelete(DeleteBehavior.NoAction);


        ConfigureAutoIncludes(builder);
    }


    private void ConfigureAutoIncludes(EntityTypeBuilder<Village> builder)
    {
        builder.Navigation(a => a.Faction).AutoInclude();
        builder.Navigation(a => a.Player).AutoInclude();

    }
}
