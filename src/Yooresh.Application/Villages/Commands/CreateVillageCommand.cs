using Yooresh.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Yooresh.Domain.Entities.Villages;

using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.Application.Villages.Commands;

public record CreateVillageCommand : IRequest<bool>
{
    public string Name { get; set; }
    public Guid PlayerId { get; set; }
    public Guid FactionId { get; set; }
}

public class CreateVillageCommandHandler : IRequestHandler<CreateVillageCommand, bool>
{
    private readonly IContext _context;

    public CreateVillageCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
    {
       // var village = await _villageFactory.CreateBasicVillageInstanceAsync(request);
       // _context.Villages.Add(village);
       // await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


}
