using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class FactionConfiguration : IEntityTypeConfiguration<Faction>
{
    public void Configure(EntityTypeBuilder<Faction> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property<string>("Advantages")
            .HasConversion<string>()
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.Property<string>("Disadvantages")
            .HasConversion<string>()
            .HasMaxLength(1000)
            .IsRequired();

        builder.Ignore(a => a.AdvantagesList);
        builder.Ignore(a => a.DisadvantagesList);
    }
}