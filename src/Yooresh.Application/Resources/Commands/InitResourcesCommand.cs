using MediatR;
using Microsoft.Extensions.Configuration;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Account;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Commands;

public class InitResourcesCommand : IRequest<List<Resource>>
{
    public Guid PlayerId { get; internal set; }
}

public class InitResourcesCommandHandler(IContext context, IConfiguration configuration) :
    IRequestHandler<InitResourcesCommand, List<Resource>>
{
    private readonly IContext _context = context;
    private readonly IConfiguration _configuration = configuration;

   // private int _startingAvailableAmount;
   // private int _startingHarvestRatePerMinute;

    public async Task<List<Resource>> Handle(
        InitResourcesCommand request, CancellationToken cancellationToken)
    {
       // GetInitialValuesFromConfiguration();
        List<Resource> resources = CreateResourcesInstances(request);
        await InsertResources(resources, cancellationToken);
        return resources;
    }

   // private void GetInitialValuesFromConfiguration()
   // {
        //_startingAvailableAmount = int.Parse(_configuration.GetSection("Resources")["StartingAvailableAmount"]!);
        //_startingHarvestRatePerMinute = int.Parse(_configuration.GetSection("Resources")["StartingHarvestRatePerMinute"]!);
  //  }

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
