using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Villages.Application.Common.Interfaces;

namespace Villages.Application.Villages.Queries.Validator;

public class GetVillageQueryValidtor : AbstractValidator<GetVillageQuery>
{
    private readonly IContext _context;

    public GetVillageQueryValidtor(IContext context)
    {
        _context = context;

        RuleFor(a => a.PlayerId)
            .NotEmpty()
            .NotNull()
            .Must(NotDefault)
            .WithMessage("Logged player is not valid")
            .MustAsync(ExistsInDatabase)
            .WithMessage("Village does not exist");
    }

    private bool NotDefault(Guid playerId)
    {
        return playerId != default;
    }

    private async Task<bool> ExistsInDatabase(Guid playerId, CancellationToken cancellationToken)
    {
        return await _context.Villages
            .AsNoTracking()
            .AnyAsync(a => a.PlayerId == playerId, cancellationToken);
    }
}