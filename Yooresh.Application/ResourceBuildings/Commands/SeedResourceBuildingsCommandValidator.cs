using Yooresh.Application.Common.Interfaces;
using FluentValidation;

namespace Yooresh.Application.ResourceBuildings.Commands;

public class SeedResourceBuildingsCommandValidator : AbstractValidator<SeedResourceBuildingsCommand>
{
    private readonly IContext _context;

    public SeedResourceBuildingsCommandValidator(IContext context)
    {
        _context = context;
    }
}