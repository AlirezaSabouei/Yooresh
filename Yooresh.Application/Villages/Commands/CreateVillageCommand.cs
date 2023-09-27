using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Villages;
using MediatR;

namespace Yooresh.Application.Villages.Commands;

public record CreateVillageCommand : IRequest<Village>
{
    public string Name { get; set; }
    public PlayerReference Player { get; set; }
}

public class CreateVillageCommandHandler : IRequestHandler<CreateVillageCommand, Village>
{
    private readonly IContext _context;

    public CreateVillageCommandHandler(IContext context)
    {
        _context = context;
    }
    
    public Task<Village> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
    {
        return null;
       // Village newVillage=new Village(request.Name,)
    }
}
