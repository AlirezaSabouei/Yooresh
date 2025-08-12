using MediatR;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Commands;

public class InitResourcesCommand : IRequest<List<Resource>>
{
    public Guid PlayerId { get; internal set; }
}

public class InitResourcesCommandHandler(IContext context) :
    IRequestHandler<InitResourcesCommand, List<Resource>>
{
    private readonly IContext _context = context;

    public async Task<List<Resource>> Handle(
        InitResourcesCommand request, CancellationToken cancellationToken)
    {

        List<Resource> resources = CreateResourcesInstances(request);
        await InsertResources(resources, cancellationToken);
        return resources;
    }

    private List<Resource> CreateResourcesInstances(InitResourcesCommand request)
    {
        var gold = CreateResourceInstance(request, ResourceType.Gold);
        var wood = CreateResourceInstance(request, ResourceType.Lumber);
        var stone = CreateResourceInstance(request, ResourceType.Stone);
        var food = CreateResourceInstance(request, ResourceType.Food);
        return [gold, wood, stone, food];
    }

    private Resource CreateResourceInstance(InitResourcesCommand request, ResourceType resourceType)
    {
        return new Resource()
        {
            Id = Guid.NewGuid(),
            AvailableAmount = 0,
            HarvestRatePerMinute = 0,
            LastUpdateDate = DateTime.UtcNow,
            Player = new PlayerReference() { Id = request.PlayerId },
            ResourceType = resourceType
        };
    }

    private async Task InsertResources(List<Resource> resources, CancellationToken cancellationToken)
    {
        await _context.Resources.AddRangeAsync(resources, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
