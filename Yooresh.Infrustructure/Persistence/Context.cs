using System.Reflection;
using MediatR;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Factions;

namespace Yooresh.Infrastructure.Persistence;

public class Context : DbContext, IContext
{
    private readonly IMediator _mediator;
    public DbSet<Player> Players { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<Faction> Factions { get; set; }
    public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
    public DbSet<VillageUpgradeQueue> ResourceBuildingUpgradeQueue { get; set; }

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