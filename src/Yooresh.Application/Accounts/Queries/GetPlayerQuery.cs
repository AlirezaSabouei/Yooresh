using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Queries;

public record GetPlayerQuery : IRequest<Player>
{
    public string Email { get; set; } = null!;
}

public class GetPlayerQueryHandler(IContext context) : IRequestHandler<GetPlayerQuery, Player>
{
    private readonly IContext _context = context;

    public async Task<Player> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _context.Players
            .Where(a=>a.Confirmed)
            .FirstOrDefaultAsync(a => a.Email == request.Email,
                cancellationToken);

        return player is null ? throw new PlayerNotExistsException() : player;
    }
}