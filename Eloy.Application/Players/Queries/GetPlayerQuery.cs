using AutoMapper;
using Eloy.Application.Common.Interfaces;
using Eloy.Domain.Entities;
using Eloy.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eloy.Application.Players.Queries;

public record GetPlayerQuery : IRequest<Player>
{
    public string Email { get; set; }
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
            .Include(a=>a.ResourceBuildings)
            .FirstOrDefaultAsync(a =>
                    string.Equals(a.Email, request.Email, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);

        if (player is null)
        {
            throw new PlayerNotExistsException();
        }

        return player;
    }
}