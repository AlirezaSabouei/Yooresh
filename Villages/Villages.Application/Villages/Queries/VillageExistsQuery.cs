using MediatR;
using Microsoft.EntityFrameworkCore;
using Villages.Application.Common.Interfaces;

namespace Villages.Application.Villages.Queries;

public record VillageExistsQuery : IRequest<bool>
{
    public Guid PlayerId { get; set; }
}

public class VillageExistsQueryHandler : IRequestHandler<VillageExistsQuery, bool>
{
    private readonly IContext _context;

    public VillageExistsQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(VillageExistsQuery request, CancellationToken cancellationToken)
    {
        var exists = await _context.Villages
            .AsNoTracking()
            .AnyAsync(a => a.PlayerId == request.PlayerId);

        return exists;
    }
}
