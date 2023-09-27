using Yooresh.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Villages.Commands;

public class CreateVillageCommandValidator : AbstractValidator<CreateVillageCommand>
{
    private readonly IContext _context;

    public CreateVillageCommandValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Village name can not be empty")
            .Length(1, 100)
            .WithMessage("Village name should be between 1 and 100");

        RuleFor(a => a.PlayerId)
            .NotNull()
            .NotEmpty()
            .WithMessage("No player is selected")
            .WithMessage("No such player exists")
            .MustAsync(BeUnique)
            .WithMessage("You already have a village");

        RuleFor(a => a.FactionId)
            .NotNull()
            .NotEmpty()
            .MustAsync(Exists)
            .WithMessage("No such faction exists");
    }

    private async Task<bool> Exists(Guid factionId, CancellationToken cancellationToken)
    {
        return await _context.Villages
            .AsNoTracking()
            .AnyAsync(a => a.FactionId == factionId, cancellationToken);
    }

    private async Task<bool> BeUnique(Guid playerId, CancellationToken cancellationToken)
    {
        return await _context.Villages
            .AsNoTracking()
            .CountAsync(a => a.FactionId == playerId, cancellationToken) == 0;
    }
}