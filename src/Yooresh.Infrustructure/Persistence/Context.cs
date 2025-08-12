using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence;

public class Context(DbContextOptions options, IMediator mediator) : DbContext(options), IContext
{
    private readonly IMediator _mediator = mediator;

    public DbSet<Player> Players { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ResourceBuildingUpgrade> ResourceBuildingUpgrades { get; set; }
    public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
    public DbSet<DefensiveBuildingUpgrade> DefensiveBuildingUpgrades { get; set; }
    public DbSet<DefensiveBuilding> DefensiveBuildings { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Faction> Factions { get; set; }

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

    public IQueryable<TEntity> QuerySet<TEntity>() where TEntity : BaseEntity
    {
        return base.Set<TEntity>();
    }
}
