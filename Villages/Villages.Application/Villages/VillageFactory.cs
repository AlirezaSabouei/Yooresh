using Microsoft.EntityFrameworkCore;
using Villages.Application.Common.Interfaces;
using Villages.Application.Villages.Commands;
using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.Entities;

namespace Villages.Application.Villages;

public class VillageFactory
{
    private readonly IContext _context;

    public VillageFactory(IContext context)
    {
        _context = context;
    }

    public async Task<Village> CreateBasicVillageInstanceAsync(CreateVillageCommand command)
    {
        var village = new Village()
        {
            Name = command.Name,
            PlayerId = command.PlayerId,
            FactionId = command.FactionId,
            AvailableBuilders = 2,
            Resource = new Resource(0, 0, 0, 0, 0)
        };
        await AddBasicResourceBuildings(village);
        await AddBasicDefensiveBuildings(village);
        return village;
    }

    private async Task AddBasicResourceBuildings(Village village)
    {
        var farm = await _context.Farms.SingleAsync(a => a.Level == 0);

        var lumberMill = await _context.LumberMills.SingleAsync(a => a.Level == 0);

        var stoneMine = await _context.StoneMines.SingleAsync(a => a.Level == 0);

        var metalMine = await _context.MetalMines.SingleAsync(a => a.Level == 0);

        var goldMine = await _context.GoldMines.SingleAsync(a => a.Level == 0);

        village.VillageResourceBuildings.Add(new VillageBuilding(village, farm));
        village.VillageResourceBuildings.Add(new VillageBuilding(village, lumberMill));
        village.VillageResourceBuildings.Add(new VillageBuilding(village, stoneMine));
        village.VillageResourceBuildings.Add(new VillageBuilding(village, metalMine));
        village.VillageResourceBuildings.Add(new VillageBuilding(village, goldMine));
    }

    private async Task AddBasicDefensiveBuildings(Village village)
    {
        var tower = await _context.Towers.SingleAsync(a => a.Level == 0);

        var wall = await _context.Walls.SingleAsync(a => a.Level == 0);

        village.VillageTower = new VillageTower {Village = village, Tower = tower};
        village.VillageWall = new VillageWall {Village = village, Wall = wall};
    }
}