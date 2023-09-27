using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Villages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Villages.Queries;

public record GetVillageQuery : IRequest<Village>
{
    public Guid PlayerId { get; set; }
}

public class GetVillageQueryHandler : IRequestHandler<GetVillageQuery, Village>
{
    private readonly IContext _context;

    public GetVillageQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Village> Handle(GetVillageQuery request, CancellationToken cancellationToken)
    {
        var village = await _context.Villages
            .Include(a => a.Player)
            .Include(a => a.Resource)
            .AsNoTracking()
            .FirstAsync(a => a.Player.Id == request.PlayerId, cancellationToken);

        return village;
    }
}