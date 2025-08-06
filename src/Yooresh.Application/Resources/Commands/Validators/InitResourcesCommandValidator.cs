using FluentValidation;

namespace Yooresh.Application.Resources.Commands.Validators;

public class InitResourcesCommandValidator : AbstractValidator<InitResourcesCommand>
{
    public InitResourcesCommandValidator()
    {
        RuleFor(a => a.PlayerId)
        .NotNull()
        .NotEmpty();
    }
}
