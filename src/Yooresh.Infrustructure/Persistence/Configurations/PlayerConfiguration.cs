using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(nameof(Player));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(a => a.Role)
            .HasColumnType("nvarchar(24)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(),         // Convert enum to string
                v => (Role)Enum.Parse(typeof(Role), v) // Parse string to enum                
            )
            .HasMaxLength(20);

        builder.Property(a => a.Confirmed)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
