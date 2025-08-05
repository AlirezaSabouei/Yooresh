using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Account.Commands;
using Yooresh.Application.Common.Interfaces;

namespace Yooresh.Application.Account.Commands.Validators;

public class ConfirmPlayerCommandValidator : AbstractValidator<ConfirmAccountCommand>
{
    private readonly IContext _context;

    public ConfirmPlayerCommandValidator(IContext context)
    {
        _context = context;

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
        var count = await _context.Players.CountAsync(a => a.Id == playerId, cancellationToken);

        return count == 1;
    }
}