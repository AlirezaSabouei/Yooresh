using Yooresh.Domain.Entities.Players;
using Microsoft.EntityFrameworkCore;
using Village = Yooresh.Domain.Entities.Villages.Village;
using Yooresh.Domain.Villages.Entities;
using Yooresh.Domain.Buildings.Entities;
using Yooresh.Domain.ResourceBuildings.Entities;
using Yooresh.Domain.DefenseBuildings.Entities;
using Yooresh.Domain.Factions.Entities;

namespace Yooresh.Application.Common.Interfaces;

public interface IContext
{
    public DbSet<Player> Players { get; set; }
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
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}