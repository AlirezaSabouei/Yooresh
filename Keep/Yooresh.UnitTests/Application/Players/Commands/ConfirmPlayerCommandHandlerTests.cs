using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.UnitTests.Application.Base;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Yooresh.Domain.Players;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class ConfirmPlayerCommandHandlerTests:
    CommandHandlerTests<ConfirmPlayerCommandHandler,ConfirmPlayerCommand,bool>
{
    private Mock<IContext>? _contextMock;
    private Mock<Player>? _playerMock;

    protected override void InitDependencies()
    {
        _contextMock = new Mock<IContext>();
        _playerMock = new Mock<Player>("name", "my@email.com", "Aa123456", Role.SimplePlayer);
    }

    protected override void SetupDependencies()
    {
        _contextMock!.Setup(a => a.Players)
            .ReturnsDbSet((new List<Player>(){_playerMock!.Object}).AsQueryable());
    }

    protected override ConfirmPlayerCommand CreateValidCommand()
    {
        return new ConfirmPlayerCommand()
        {
            PlayerId = _playerMock!.Object.Id
        };
    }

    protected override ConfirmPlayerCommandHandler CreateCommandHandler()
    {
        return new ConfirmPlayerCommandHandler(
            _contextMock!.Object
        );
    }

    [Fact]
    public void Handle_CommandIsValid_ContextSaveChangesIsCalled()
    {
        Handler!.Handle(Command!, new CancellationToken());

        _contextMock!
            .Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
    }
    
    [Fact]
    public void Handle_CommandIsValid_PlayerConfirmIsCalled()
    {
        Handler!.Handle(Command!, new CancellationToken());

        _playerMock!
            .Verify(x=>x.ConfirmPlayer(),Times.Once);
    }
}