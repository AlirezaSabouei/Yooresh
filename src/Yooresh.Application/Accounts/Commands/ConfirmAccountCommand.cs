using MediatR;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Commands;

public record ConfirmAccountCommand : IRequest<bool>
{
    public Guid PlayerId { get; set; }
    public required string ConfirmationCode { get; set; }
}

public class ConfirmAccountCommandHandler(IDocumentStore<Player> documentStore)
    : IRequestHandler<ConfirmAccountCommand, bool>
{
    private readonly IDocumentStore<Player> _documentStore = documentStore;

    public async Task<bool> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        var player = await _documentStore.GetByIdAsync(request.PlayerId, cancellationToken)
            ?? throw new InvalidOperationException($"Player '{request.PlayerId}' was not found.");

        player.ConfirmPlayer(request.ConfirmationCode);
        if (player.Confirmed)
        {
            await _documentStore.UpdateAsync(player, cancellationToken);
            return true;
        }
        return false;
    }
}
