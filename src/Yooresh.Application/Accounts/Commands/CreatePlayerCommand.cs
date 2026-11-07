using MediatR;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.Application.Accounts.Commands;

public record CreatePlayerCommand : IRequest<Player>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CreatePlayerCommandHandler(
    IDocumentStore<Player> documentStore, IPasswordEncryption<BaseEntity> passwordEncryption)
    : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly IDocumentStore<Player> _documentStore = documentStore;
    private readonly IPasswordEncryption<BaseEntity> _passwordEncryption = passwordEncryption;

    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player(request.Name, request.Email, request.Password);
        player.Password = _passwordEncryption.HashPassword(player, request.Password);
        player.AddDomainEvent(new PlayerCreatedEvent(player));
        await _documentStore.InsertAsync(player, cancellationToken);
        return player;
    }
}
