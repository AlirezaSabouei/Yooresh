using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yooresh.Application.Common.Interfaces;

namespace Yooresh.Application.Villages.Queries;

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
