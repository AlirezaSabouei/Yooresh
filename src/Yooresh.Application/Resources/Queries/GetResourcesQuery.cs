using MediatR;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Queries;

public record GetResourcesQuery : IRequest<List<Resource>>
{
    public Guid PlayerId { get; set; }
}

public class GetResourcesQueryHandler(IDocumentStore<Resource> documentStore, IDateTimeProvider dateTimeProvider)
    : IRequestHandler<GetResourcesQuery, List<Resource>>
{
    private readonly IDocumentStore<Resource> _documentStore = documentStore;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<List<Resource>> Handle(
        GetResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _documentStore.GetAsync(a => a.Player.Id == request.PlayerId, cancellationToken);
        resources.ForEach(resource => resource.CalculateAvailableAmount(_dateTimeProvider.GetUtcNow()));
        return resources;
    }
}
