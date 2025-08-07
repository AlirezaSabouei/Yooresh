using FluentValidation;

namespace Yooresh.Application.Accounts.Queries.Validators;

public class LoginQueryValidator : AbstractValidator<GetPlayerQuery>
{
    public LoginQueryValidator()
    {

        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage(a => "Login Info Is Not Valid");
    }
}