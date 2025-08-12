using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;
using Yooresh.Domain.Entities.DefensiveBuildings;

namespace Yooresh.Application.Common.Interfaces;

public interface IContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ResourceBuildingUpgrade> ResourceBuildingUpgrades { get; set; }
    public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
    public DbSet<DefensiveBuildingUpgrade> DefensiveBuildingUpgrades { get; set; }
    public DbSet<DefensiveBuilding> DefensiveBuildings { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Faction> Factions { get; set; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<TEntity> QuerySet<TEntity>()
    where TEntity : BaseEntity;
}
