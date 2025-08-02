using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using MediatR;
using Yooresh.Domain.Events;
using Yooresh.Application.Common.Tools;

namespace Yooresh.Application.Players.Commands;

public record CreatePlayerCommand : IRequest<Player>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CreatePlayerCommandHandler(
    IContext context, IPasswordEncryption<Player> passwordEncryption)
    : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly IContext _context = context;
    private readonly IPasswordEncryption<Player> _passwordEncryption = passwordEncryption;

    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player()
        {
            Name = request.Name,
            Email = request.Email.Trim().ToLowerInvariant(),
            Password = request.Password,
            Role = Role.SimplePlayer,
            Confirmed = false,
            ConfirmationCode = Guid.NewGuid().ToString()
        };
        player.Password = _passwordEncryption.HashPassword(player, player.Password);
        await _context.Players.AddAsync(player, cancellationToken);
        player.AddDomainEvent(new PlayerCreatedEvent(player));
        await _context.SaveChangesAsync(cancellationToken);
        return player;
    }
}