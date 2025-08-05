using FluentValidation;

namespace Yooresh.Application.Resources.Commands.Validators;

public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    public CreateResourceCommandValidator()
    {
        RuleFor(a => a.StartingAvailableAmount)
            .GreaterThanOrEqualTo(1);

        RuleFor(a => a.StartingHarvestRatePerMinute)
            .GreaterThanOrEqualTo(1);

        RuleFor(a => a.PlayerId)
        .NotNull()
        .NotEmpty();
    }
}
