using MediatR;
using Yooresh.Application.DefensiveBuildings.Commands;
using Yooresh.Application.ResourceBuildings.Commands;
using Yooresh.Application.Resources.Commands;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Accounts.Commands;

public class InitPlayerAssetsCommand : IRequest
{
    public Guid PlayerId { get; set; }
}

public class InitPlayerAssetsCommandHandler(
    IRequestHandler<InitResourcesCommand, List<Resource>> initResourcesCommandHandler,
    IRequestHandler<InitResourceBuildingsCommand, List<ResourceBuilding>> initResourceBuildingsCommandHandler,
    IRequestHandler<InitDefensiveBuildingsCommand, List<DefensiveBuilding>> initDefensiveBuildingsCommandHandler) : IRequestHandler<InitPlayerAssetsCommand>
{
    private readonly IRequestHandler<InitResourcesCommand, List<Resource>> _initResourceCommandHandler =
    initResourcesCommandHandler;
    private readonly IRequestHandler<InitResourceBuildingsCommand, List<ResourceBuilding>> _initResourceBuildingsCommandHandler =
        initResourceBuildingsCommandHandler;
    private readonly IRequestHandler<InitDefensiveBuildingsCommand, List<DefensiveBuilding>> _initDefensiveBuildingsCommandHandler =
        initDefensiveBuildingsCommandHandler;

    private Guid _playerId;

    public async Task Handle(InitPlayerAssetsCommand request, CancellationToken cancellationToken)
    {
        _playerId = request.PlayerId;
        await InitiateResourcesForPlayer(cancellationToken);
        await InitiateResourceBuildingsForPlayer(cancellationToken);
        await InitiateDefensiveBuildingsForPlayer(cancellationToken);
    }

    private async Task InitiateResourcesForPlayer(CancellationToken cancellationToken)
    {
        InitResourcesCommand command = BuildInitResourcesCommand();
        await _initResourceCommandHandler.Handle(command, cancellationToken);
    }

    private InitResourcesCommand BuildInitResourcesCommand()
    {
        return new()
        {
            PlayerId = _playerId
        };
    }

    private async Task InitiateResourceBuildingsForPlayer(CancellationToken cancellationToken)
    {
        InitResourceBuildingsCommand command = BuildInitResourceBuildingsCommand();
        await _initResourceBuildingsCommandHandler.Handle(command, cancellationToken);
    }

    private InitResourceBuildingsCommand BuildInitResourceBuildingsCommand()
    {
        return new()
        {
            PlayerId = _playerId
        };
    }

    private async Task InitiateDefensiveBuildingsForPlayer(CancellationToken cancellationToken)
    {
        InitDefensiveBuildingsCommand command = BuildInitDefensiveBuildingsCommand();
        await _initDefensiveBuildingsCommandHandler.Handle(command, cancellationToken);
    }

    private InitDefensiveBuildingsCommand BuildInitDefensiveBuildingsCommand()
    {
        return new()
        {
            PlayerId = _playerId
        };
    }
}
