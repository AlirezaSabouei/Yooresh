using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.Application.Account.Commands.Validators;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    private readonly IContext _context;

    public CreatePlayerCommandValidator(IContext context)
    {
        _context = context;

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