using Villages.Application.Common.Interfaces;
using MediatR;

namespace Villages.Application.Villages.Commands;

public record CreateVillageCommand : IRequest<bool>
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Guid FactionId { get; set; }
}

public class CreateVillageCommandHandler : IRequestHandler<CreateVillageCommand, bool>
{
    private readonly IContext _context;
    private readonly VillageFactory _villageFactory;

    public CreateVillageCommandHandler(IContext context,VillageFactory villageFactory)
    {
        _context = context;
        _villageFactory = villageFactory;
    }

    public async Task<bool> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
    {
        var village = await _villageFactory.CreateBasicVillageInstanceAsync(request);
        _context.Villages.Add(village);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


}
