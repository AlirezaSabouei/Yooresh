using FluentValidation;

namespace Yooresh.Application.Resources.Queries.Validators;

public class GetResourcesQueryValidator : AbstractValidator<GetResourcesQuery>
{
    public GetResourcesQueryValidator()
    {

        RuleFor(a => a.PlayerId)
            .NotEmpty()
            .WithMessage($"player is not valid");
    }
}