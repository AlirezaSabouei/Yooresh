using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
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
            .IsEncrypted()
            .HasMaxLength(50);
        
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