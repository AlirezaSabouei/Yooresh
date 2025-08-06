using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Villages.Commands;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Application.Villages;

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
            Resource = new Domain.Entities.Resources.ResourceValueObject(0, 0, 0, 0)
        };
        await AddBasicDefensiveBuildings(village);
        return village;
    }

    private async Task AddBasicDefensiveBuildings(Village village)
    {
        var tower = await _context.Towers.SingleAsync(a => a.Level == 0);

        var wall = await _context.Walls.SingleAsync(a => a.Level == 0);

        village.VillageTower = new VillageTower {Village = village, Tower = tower};
        village.VillageWall = new VillageWall {Village = village, Wall = wall};
    }
}