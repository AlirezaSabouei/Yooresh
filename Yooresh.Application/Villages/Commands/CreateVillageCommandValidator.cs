using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Villages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Villages.Commands;

public class CreateVillageCommandValidator : AbstractValidator<CreateVillageCommand>
{
    private readonly IContext _context;

    public CreateVillageCommandValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.Player)
            .NotNull()
            .WithMessage("No player is selected")
            .Must(NotDefault)
            .WithMessage("No such player exists")
            .MustAsync(BeUnique)
            .WithMessage("You already have a village");

        RuleFor(a => a.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Village name can not be empty")
            .Length(1, 100)
            .WithMessage("Village name should be between 1 and 100");
    }

    private bool NotDefault(PlayerReference player)
    {
        return player.Id != default;
    }

    private async Task<bool> BeUnique(PlayerReference player, CancellationToken cancellationToken)
    {
        return await _context.Villages
            .AsNoTracking()
            .CountAsync(a => a.Player.Id == player.Id, cancellationToken) == 0;
    }
}