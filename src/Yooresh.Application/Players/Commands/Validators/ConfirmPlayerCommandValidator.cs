using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;

namespace Yooresh.Application.Players.Commands.Validators;

public class ConfirmPlayerCommandValidator : AbstractValidator<ConfirmPlayerCommand>
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