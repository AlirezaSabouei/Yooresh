using MediatR;
using Yooresh.Application.Common.Interfaces;

namespace Yooresh.Application.ResourceBuildings.Commands;

//public record UpdateResourceBuildingCommand : IRequest<bool>
//{
//    public Guid PlayerId { get; set; }
//    public Guid ResourceBuildingId { get; set; }
//}

//public class UpdateResourceBuildingCommandHandler : IRequestHandler<UpdateResourceBuildingCommand, bool>
//{
//    private readonly IContext _context;
//    private readonly IRequestHandler<GetVillageQuery, Village> _getVillageQueryHandler;

//    public UpdateResourceBuildingCommandHandler(
//        IContext context, 
//        IRequestHandler<GetVillageQuery, Village> getVillageQueryHandler)
//    {
//        _context = context;
//        _getVillageQueryHandler = getVillageQueryHandler;
//    }

//    public async Task<bool> Handle(UpdateResourceBuildingCommand request, CancellationToken cancellationToken)
//    {
//        //var getVillageQuery = new GetVillageQuery()
//        //{
//        //    PlayerId = request.PlayerId
//        //};
//        //var village = await _getVillageQueryHandler.Handle(getVillageQuery, cancellationToken);

//        //village.VillageResourceBuildings
//        //    .First(a=>a.BuildingId==request.ResourceBuildingId)
//        //    .Building
//        //    .StartUpgrade(village);

//        //_context.Villages.Attach(village);

//        //await _context.SaveChangesAsync(cancellationToken);

//        return true;
//    }
//}