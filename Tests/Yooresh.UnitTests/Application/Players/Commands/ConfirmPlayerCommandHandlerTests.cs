using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class ConfirmPlayerCommandHandlerTests:
    RequestHandlerTests<ConfirmPlayerCommandHandler,ConfirmPlayerCommand,bool>
{
    private Mock<IContext>? _contextMock;
    private Mock<Player>? _playerMock;

    protected override Task InitDependenciesAsync()
    {
        _contextMock = new Mock<IContext>();
        _playerMock = new Mock<Player>("name", "my@email.com", "Aa123456", Role.SimplePlayer);
        return Task.CompletedTask;
    }

    protected override Task SetupDependenciesAsync()
    {
        _contextMock!.Setup(a => a.Players)
            .ReturnsDbSet((new List<Player>(){_playerMock!.Object}).AsQueryable());
        return Task.CompletedTask;
    }

    protected override ConfirmPlayerCommand CreateValidRequest()
    {
        return new ConfirmPlayerCommand()
        {
            PlayerId = _playerMock!.Object.Id
        };
    }

    protected override ConfirmPlayerCommandHandler CreateRequestHandler()
    {
        return new ConfirmPlayerCommandHandler(
            _contextMock!.Object
        );
    }

    //[Fact]
    //public void Handle_CommandIsValid_ContextSaveChangesIsCalled()
    //{
    //    Handler!.Handle(Command!, new CancellationToken());

    //    _contextMock!
    //        .Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
    //}
    
    //[Fact]
    //public void Handle_CommandIsValid_PlayerConfirmIsCalled()
    //{
    //    Handler!.Handle(Command!, new CancellationToken());

    //    _playerMock!
    //        .Verify(x=>x.ConfirmPlayer(),Times.Once);
    //}
}