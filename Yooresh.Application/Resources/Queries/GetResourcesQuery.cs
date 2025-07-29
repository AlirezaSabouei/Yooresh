using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Queries;

public record GetResourcesQuery : IRequest<List<Resource>>
{
    public Guid PlayerId { get; set; }
}

public class GetResourcesQueryHandler(IContext context, IDateTimeProvider dateTimeProvider)
    : IRequestHandler<GetResourcesQuery, List<Resource>>
{
    private readonly IContext _context = context;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<List<Resource>> Handle(
        GetResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _context.Resources.Where(a=>a.Player.Id ==  request.PlayerId).ToListAsync();
        resources.ForEach(resource => resource.CalculateAvailableAmount(_dateTimeProvider.GetUtcNow()));
        return resources;
    }
}
