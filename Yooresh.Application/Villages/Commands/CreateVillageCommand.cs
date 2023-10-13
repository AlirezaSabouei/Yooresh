using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Villages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Enums;

namespace Yooresh.Application.Villages.Commands;

public record CreateVillageCommand : IRequest<bool>
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Guid FactionId { get; set; }
}

public class CreateVillageCommandHandler : IRequestHandler<CreateVillageCommand, bool>
{
    private readonly IContext _context;

    public CreateVillageCommandHandler(IContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
    {
        var village = new Village()
        {
            Name = request.Name,
            PlayerId = request.PlayerId,
            FactionId = request.FactionId
        };
        await AddBasicResourceBuildings(village);
        _context.Villages.Add(village);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task AddBasicResourceBuildings(Village village)
    {
        var farm = await _context.ResourceBuildings.SingleAsync(a =>
            a.ProductionType == ResourceType.Food && a.Level == 0);
        farm.LastResourceGatherDate = DateTimeOffset.UtcNow;
        
        var lumberMill = await _context.ResourceBuildings.SingleAsync(a =>
            a.ProductionType == ResourceType.Food && a.Level == 0);
        lumberMill.LastResourceGatherDate = DateTimeOffset.UtcNow;
        
        var stoneMine = await _context.ResourceBuildings.SingleAsync(a =>
            a.ProductionType == ResourceType.Food && a.Level == 0);
        stoneMine.LastResourceGatherDate = DateTimeOffset.UtcNow;
        
        var metalMine = await _context.ResourceBuildings.SingleAsync(a =>
            a.ProductionType == ResourceType.Food && a.Level == 0);
        metalMine.LastResourceGatherDate = DateTimeOffset.UtcNow;
        
        village.ResourceBuildings.Add(farm);
        village.ResourceBuildings.Add(lumberMill);
        village.ResourceBuildings.Add(stoneMine);
        village.ResourceBuildings.Add(metalMine);
    }
}
