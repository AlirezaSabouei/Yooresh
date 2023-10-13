using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Enums;

namespace Yooresh.Application.Villages.Commands;

public record UpdateResourceBuildingCommand : IRequest<bool>
{
    public Guid PlayerId { get; set; }
    public Guid ResourceBuildingId { get; set; }
}

public class UpdateResourceBuildingCommandHandler : IRequestHandler<UpdateResourceBuildingCommand, bool>
{
    private readonly IContext _context;

    public UpdateResourceBuildingCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateResourceBuildingCommand request, CancellationToken cancellationToken)
    {
        var village = (await _context.Villages
                .FirstAsync(a => a.PlayerId == request.PlayerId, cancellationToken))
            .GatherResources()
            .ApplyFinishedUpgrades();

        var building = await _context.ResourceBuildings.FindAsync(request.ResourceBuildingId, cancellationToken);

        building!.StartUpgrade(village);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}