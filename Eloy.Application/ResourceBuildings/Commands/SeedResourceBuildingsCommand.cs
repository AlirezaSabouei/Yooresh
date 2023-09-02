using Eloy.Application.Common.Interfaces;
using Eloy.Domain.Entities.ResourceBuildings;
using Eloy.Domain.Entities.ResourceBuildings.Extends;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eloy.Application.ResourceBuildings.Commands;

public record SeedResourceBuildingsCommand:IRequest<List<ResourceBuilding>>
{
}

public class SeedResourceBuildingsCommandHandler : IRequestHandler<SeedResourceBuildingsCommand, List<ResourceBuilding>>
{
    private readonly IContext _context;

    public SeedResourceBuildingsCommandHandler(IContext context)
    {
        _context = context;
    }
    
    public async Task<List<ResourceBuilding>> Handle(SeedResourceBuildingsCommand request, CancellationToken cancellationToken)
    {
        if (await _context.ResourceBuildings.AnyAsync(cancellationToken))
        {
            return await _context.ResourceBuildings.ToListAsync(cancellationToken);
        }
        
        SeedBuildings<Farm>();        
        SeedBuildings<StoneMine>();
        SeedBuildings<Lumbermill>();
        SeedBuildings<GoldMine>();
        SeedBuildings<MetalMine>();
        await _context.SaveChangesAsync(cancellationToken);
        return await _context.ResourceBuildings.ToListAsync(cancellationToken);
    }

    private void SeedBuildings<T>() where T : ResourceBuilding
    {
        var buildings = new List<T>();
        
        for (int i = 0; i < 15; i++)
        {
            var building = Activator.CreateInstance<T>();
            building.Level = i;
            building.ProductionRatePerHour = i * 10;
            
            buildings.Add(building);
        }
        
        _context.ResourceBuildings.AddRange(buildings);
    }
}