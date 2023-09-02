using Eloy.Application.Common.Interfaces;
using FluentValidation;

namespace Eloy.Application.Players.Queries;

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