using Yooresh.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Players.Commands;

public record ConfirmPlayerCommand : IRequest<bool>
{
    public Guid PlayerId { get; set; }
}

public class ConfirmPlayerCommandHandler : IRequestHandler<ConfirmPlayerCommand, bool>
{
    private readonly IContext _context;

    public ConfirmPlayerCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ConfirmPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players.FirstAsync(a => a.Id == request.PlayerId, cancellationToken);
        player.ConfirmPlayer();
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}