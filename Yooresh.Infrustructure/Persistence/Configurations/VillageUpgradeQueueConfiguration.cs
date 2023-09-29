using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class VillageUpgradeQueueConfiguration : IEntityTypeConfiguration<VillageUpgradeQueue>
{
    public void Configure(EntityTypeBuilder<VillageUpgradeQueue> builder)
    {
        builder.ToTable(nameof(VillageUpgradeQueue));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();

        builder.Property(a => a.ToId)
            .IsRequired();
        
        builder.Property(a => a.StartTime)
            .IsRequired();
        
        builder.Property(a => a.EndTime)
            .IsRequired();

        builder.Property(a => a.Completed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Ignore(a => a.IsFinished);
        
        builder.Property(a => a.UpgradeType)
            .HasColumnType("nvarchar(50)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(),         // Convert enum to string
                v => (UpgradeType)Enum.Parse(typeof(UpgradeType), v) // Parse string to enum                
            )
            .HasMaxLength(50);
    }
}