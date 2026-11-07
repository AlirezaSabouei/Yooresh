using FluentValidation;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.Application.Accounts.Commands.Validators;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    private readonly IDocumentStore<Player> _documentStore;

    public CreatePlayerCommandValidator(IDocumentStore<Player> documentStore)
    {
        _documentStore = documentStore;

        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MustAsync(BeUniqueInDatabase)
            .WithMessage("'{PropertyName}' is already in use");

        RuleFor(a => a.Password)
            .NotEmpty()
            .NotNull()
            .Must(MeetPasswordStrength)
            .WithMessage(
                "'{PropertyName}' should be at least 5 characters and contain both letters and numbers");
    }

    private async Task<bool> BeUniqueInDatabase(CreatePlayerCommand request, string property,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return false;
        }

        var players = await _documentStore.GetAsync(_ => true, cancellationToken);
        return !players.Any(a =>
            string.Equals(a.Email, request.Email.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private bool MeetPasswordStrength(CreatePlayerCommand request, string property)
    {
        return !string.IsNullOrWhiteSpace(request.Password) &&
               request.Password.Length > 5 &&
               request.Password.Any(c => char.IsLetter(c)) &&
               request.Password.Any(c => char.IsDigit(c));
    }
}
