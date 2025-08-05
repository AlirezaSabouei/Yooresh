using MediatR;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Commands;

public class CreateResourceCommand : IRequest<List<Resource>>
{
    public int StartingAvailableAmount { get; set; }
    public int StartingHarvestRatePerMinute { get; set; }
    public Guid PlayerId { get; internal set; }
}

public class CreateResourceCommandHandler(IContext context) : IRequestHandler<CreateResourceCommand, List<Resource>>
{
    private readonly IContext _context = context;

    public async Task<List<Resource>> Handle(
        CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var gold = CreateResourceInstance(request, ResourceType.Gold);
        var wood = CreateResourceInstance(request, ResourceType.Lumber);
        var stone = CreateResourceInstance(request, ResourceType.Stone);
        var food = CreateResourceInstance(request, ResourceType.Food);
        await _context.Resources.AddRangeAsync(gold, wood, stone, food);
        await _context.SaveChangesAsync(cancellationToken);
        return [gold, wood, stone, food];
    }

    private Resource CreateResourceInstance(CreateResourceCommand request, ResourceType resourceType)
    {
        return new Resource()
        {
            Id = Guid.NewGuid(),
            AvailableAmount = request.StartingAvailableAmount,
            HarvestRatePerMinute = request.StartingHarvestRatePerMinute,
            LastUpdateDate = DateTime.UtcNow,
            Player = new PlayerReference() { Id = request.PlayerId},
            ResourceType = resourceType
        };
    }
}
