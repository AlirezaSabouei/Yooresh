using Yooresh.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Account.Commands;

public record ConfirmAccountCommand : IRequest<bool>
{
    public Guid PlayerId { get; set; }
    public required string ConfirmationCode { get; set; }
}

public class ConfirmAccountCommandHandler(IContext context) 
    : IRequestHandler<ConfirmAccountCommand, bool>
{
    private readonly IContext _context = context;

    public async Task<bool> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
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