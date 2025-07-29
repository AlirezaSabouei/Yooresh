using Yooresh.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Players.Commands;

public record ConfirmPlayerCommand : IRequest<bool>
{
    public Guid PlayerId { get; set; }
    public string ConfirmationCode { get; set; }
}

public class ConfirmPlayerCommandHandler(IContext context) 
    : IRequestHandler<ConfirmPlayerCommand, bool>
{
    private readonly IContext _context = context;

    public async Task<bool> Handle(ConfirmPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players.FirstAsync(a => a.Id == request.PlayerId, cancellationToken);
        player.ConfirmPlayer(request.ConfirmationCode);
        if (player.Confirmed) 
        {
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }        
        return false;
    }
}