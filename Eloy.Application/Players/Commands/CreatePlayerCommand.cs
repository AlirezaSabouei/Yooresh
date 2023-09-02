using Eloy.Application.Common.Interfaces;
using Eloy.Domain.Entities;
using Eloy.Domain.Entities.ResourceBuildings;
using Eloy.Domain.Entities.ResourceBuildings.Extends;
using MediatR;

namespace Eloy.Application.Players.Commands;

public record CreatePlayerCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswrodConfirmation { get; set; }
}

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, bool>
{
    private readonly IContext _context;

    public CreatePlayerCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var newPlayer = new Player()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Confirmed = false,
            VillageName = $"{request.Name}-Village",
            ResourceBuildings = new List<ResourceBuilding>(),
            
        };

        _context.Players.Add(newPlayer);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}