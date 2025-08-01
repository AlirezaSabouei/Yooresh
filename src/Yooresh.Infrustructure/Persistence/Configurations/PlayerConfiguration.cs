using Yooresh.Domain.Entities.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .IsEncrypted()
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

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Player> builder)
    {
        var player = new Player()
        {
            Name = "Alireza Sabouei",
            Email = "alireza.sabouei@gmail.com",
            Password = "Aa123456",
            Role = Role.SimplePlayer,
            ConfirmationCode = "123",
            Id = new Guid("a58bef94-8437-4856-bd87-a7b861285773")
        };
        player.ConfirmPlayer("123");
        builder.HasData(player);
    }
}
