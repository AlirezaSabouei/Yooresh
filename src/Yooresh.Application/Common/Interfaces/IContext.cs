using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Villages;
using Village = Yooresh.Domain.Entities.Villages.Village;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.ResourceBuildingUpgrades;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Common.Interfaces;

public interface IContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ResourceBuildingUpgrade> ResourceBuildingUpgrades { get; set; }
    public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<Faction> Factions { get; set; }
    public DbSet<VillageBuilding> VillageBuildings { get; set; }
    public DbSet<Wall> Walls { get; set; }
    public DbSet<Tower> Towers { get; set; }
    public DbSet<VillageWall> VillageWalls { get; set; }
    public DbSet<VillageTower> VillageTowers { get; set; }
    
    
    public DbSet<Building> Buildings { get; set; }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<TEntity> QuerySet<TEntity>()
    where TEntity : BaseEntity;
}