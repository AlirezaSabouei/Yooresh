using System.Reflection;
using MediatR;
using Villages.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Villages.Domain.Common.Entities;
using Villages.Domain.Core.Factions.Entities;
using Villages.Domain.Core.Entities;
using Villages.Domain.Core.DefenseBuildings.Entities;
using Villages.Domain.Core.Buildings.Entities;
using Villages.Domain.Core.ResourceBuildings.Entities;

namespace Villages.Infrastructure.Persistence;

public class Context : DbContext, IContext
{
    private readonly IMediator _mediator;
    public DbSet<Village> Villages { get; set; }
    public DbSet<Faction> Factions { get; set; }
    public DbSet<VillageBuilding> VillageBuildings { get; set; }
    public DbSet<Wall> Walls { get; set; }
    public DbSet<Tower> Towers { get; set; }
    public DbSet<VillageWall> VillageWalls { get; set; }
    public DbSet<VillageTower> VillageTowers { get; set; }


    public DbSet<Building> Buildings { get; set; }
    public DbSet<Farm> Farms { get; set; }
    public DbSet<LumberMill> LumberMills { get; set; }
    public DbSet<StoneMine> StoneMines { get; set; }
    public DbSet<MetalMine> MetalMines { get; set; }
    public DbSet<GoldMine> GoldMines { get; set; }

    public Context(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }

    public string Test(Village entity)
    {
        return Entry(entity).State.ToString();
    }
}