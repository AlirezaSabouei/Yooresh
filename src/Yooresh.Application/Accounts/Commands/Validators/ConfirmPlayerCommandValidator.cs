using FluentValidation;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Commands.Validators;

public class ConfirmPlayerCommandValidator : AbstractValidator<ConfirmAccountCommand>
{
    private readonly IDocumentStore<Player> _documentStore;

    public ConfirmPlayerCommandValidator(IDocumentStore<Player> documentStore)
    {
        _documentStore = documentStore;

        RuleFor(a => a.PlayerId)
            .NotEmpty()
            .WithMessage("'{PropertyName}' Should be provided")
            .MustAsync(ExistsInDatabase)
            .WithMessage("'{PropertyName}' Does not exists");

        RuleFor(a => a.ConfirmationCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("'{PropertyName}' Should be provided");
    }

    private async Task<bool> ExistsInDatabase(Guid playerId, CancellationToken cancellationToken)
    {
        var player = await _documentStore.GetByIdAsync(playerId, cancellationToken);

        return player is not null;
    }
}
