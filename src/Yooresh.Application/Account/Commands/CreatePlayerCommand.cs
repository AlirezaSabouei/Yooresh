using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using MediatR;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Application.Account.Commands;

public record CreatePlayerCommand : IRequest<Player>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CreatePlayerCommandHandler(
    IContext context, IPasswordEncryption<BaseEntity> passwordEncryption)
    : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly IContext _context = context;
    private readonly IPasswordEncryption<BaseEntity> _passwordEncryption = passwordEncryption;

    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player(request.Name, request.Email, request.Password);
        player.Password = _passwordEncryption.HashPassword(player, request.Password);
        await _context.Players.AddAsync(player, cancellationToken);
        player.AddDomainEvent(new PlayerCreatedEvent(player));
        await _context.SaveChangesAsync(cancellationToken);
        return player;
    }
}