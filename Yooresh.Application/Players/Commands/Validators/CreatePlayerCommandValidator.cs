using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Players.Commands.Validators;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    private readonly IContext _context;

    public CreatePlayerCommandValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("'{PropertyName}' should be provided");

        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("'{PropertyName}' should be provided")
            .EmailAddress()
            .WithMessage("'{PropertyName}' should be a valid email address")
            .MustAsync(BeUniqueInDatabase)
            .WithMessage("'{PropertyName}' is already in use");

        RuleFor(a => a.Password)
            .Must(MeetPasswordStrength)
            .WithMessage(
                "'{PropertyName}' should be at least 5 chars consist of alphabet, Number and signs");
    }

    private async Task<bool> BeUniqueInDatabase(CreatePlayerCommand request, string property,
        CancellationToken cancellationToken)
    {
        var count = await _context.QuerySet<Player>().CountAsync(a => a.Email.ToLower() == request.Email.ToLower(),
            cancellationToken);

        return count == 0;
    }

    private bool MeetPasswordStrength(CreatePlayerCommand request, string property)
    {
        return !string.IsNullOrWhiteSpace(request.Password) &&
               request.Password.Length > 5 &&
               request.Password.Any(c => char.IsLetter(c)) &&
               request.Password.Any(c => char.IsDigit(c));
    }
}