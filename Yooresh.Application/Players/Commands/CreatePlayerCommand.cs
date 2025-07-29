using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using MediatR;
using Yooresh.Domain.Events;

namespace Yooresh.Application.Players.Commands;

public record CreatePlayerCommand : IRequest<Player>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CreatePlayerCommandHandler(
    IContext context)
    : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly IContext _context = context;

    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player()
        {
            Name = request.Name,
            Email = request.Email.ToLower(),
            Password = request.Password,
            Role = Role.SimplePlayer,
            ConfirmationCode = (new Random(DateTime.Now.Millisecond).Next(12345678, 99999999)).ToString()
        };
        await _context.Players.AddAsync(player, cancellationToken);
        player.AddDomainEvent(new PlayerCreatedEvent(player));
        await _context.SaveChangesAsync(cancellationToken);
        return player;
    }
}