using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Infrastructure.Persistence.Configurations;

public class FactionConfiguration : IEntityTypeConfiguration<Faction>
{
    public void Configure(EntityTypeBuilder<Faction> builder)
    {
        builder.ToTable(nameof(Faction));

        builder.HasKey(nameof(Faction.Id));
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .HasConversion<string>()
            .HasMaxLength(20)
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

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Faction> builder)
    {
        builder.HasData(
            CreateOrcsFaction(),
            CreateHumansFaction(),
            CreateElfFaction(),
            CreateUndeadFaction()
        );
    }

    private Faction CreateOrcsFaction()
    {
        const string name = "Orc";
        
        var advantages = new List<string>
        {
            "Advantage1",
            "Advantage2"
        };

        var disadvantages = new List<string>
        {
            "Disadvantage1",
            "Disadvantage2"
        };

        return new Faction(name, advantages, disadvantages,new Guid("012FC556-7788-42B7-82F7-F093D37EC517"));
    }
    
    private Faction CreateHumansFaction()
    {
        const string name = "Human";
        
        var advantages = new List<string>
        {
            "Advantage1",
            "Advantage2"
        };

        var disadvantages = new List<string>
        {
            "Disadvantage1",
            "Disadvantage2"
        };

        return new Faction(name, advantages, disadvantages,new Guid("52559F4B-4052-4438-B5B9-9BAC92DC75FB"));
    }
    
    private Faction CreateUndeadFaction()
    {
        const string name = "Undead";
        
        var advantages = new List<string>
        {
            "Advantage1",
            "Advantage2"
        };

        var disadvantages = new List<string>
        {
            "Disadvantage1",
            "Disadvantage2"
        };

        return new Faction(name, advantages, disadvantages,new Guid("56D9E842-3C04-4741-9CDA-4BFE38AA1F57"));
    }
    
    private Faction CreateElfFaction()
    {
        const string name = "Elf";
        
        var advantages = new List<string>
        {
            "Advantage1",
            "Advantage2"
        };

        var disadvantages = new List<string>
        {
            "Disadvantage1",
            "Disadvantage2"
        };

        return new Faction(name, advantages, disadvantages,new Guid("1A060E07-FD38-4C1B-96E8-B8DAEE50421E"));
    }
}