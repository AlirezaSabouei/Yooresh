using AutoMapper;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Account.Queries;

public record GetPlayerQuery : IRequest<Player>
{
    public string Email { get; set; } = null!;
}

public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, Player>
{
    private readonly IContext _context;

    public GetPlayerQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Player> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _context.Players
            .Where(a=>a.Confirmed)
            .FirstOrDefaultAsync(a => a.Email == request.Email,
                cancellationToken);

        if (player is null)
        {
            throw new PlayerNotExistsException();
        }

        return player;
    }
}