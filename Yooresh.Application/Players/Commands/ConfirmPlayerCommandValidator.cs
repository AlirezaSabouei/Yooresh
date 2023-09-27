using Yooresh.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Players.Commands;

public class ConfirmPlayerCommandValidator : AbstractValidator<ConfirmPlayerCommand>
{
    private readonly IContext _context;

    public ConfirmPlayerCommandValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.PlayerId)
            .NotEqual(new Guid())
            .WithMessage($"Should be provided")
            .MustAsync(ExistsInDatabase)
            .WithMessage($"Does not exists");
    }

    private async Task<bool> ExistsInDatabase(Guid playerId, CancellationToken cancellationToken)
    {
        var count = await _context.Players.CountAsync(a => a.Id == playerId, cancellationToken);

        return count == 1;
    }
}