using FluentValidation;

namespace Yooresh.Application.Accounts.Commands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("'{PropertyName}' should be provided")
            .EmailAddress()
            .WithMessage("'{PropertyName}' should be a valid email address");

        RuleFor(a => a.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("'{PropertyName}' should be provided");
    }
}
