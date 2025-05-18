using FluentValidation;
using Yooresh.Application.Common.Interfaces;

namespace Yooresh.Application.Players.Queries.Validators;

public class LoginQueryValidator : AbstractValidator<GetPlayerQuery>
{
    private readonly IContext _context;

    public LoginQueryValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage(a => "Login Info Is Not Valid");
    }
}