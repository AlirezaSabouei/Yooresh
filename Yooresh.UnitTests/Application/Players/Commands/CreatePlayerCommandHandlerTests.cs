using Microsoft.Extensions.Configuration;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using MockQueryable.NSubstitute;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using NSubstitute;
using NUnit.Framework;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandHandlerTests:
    CommandHandlerTests<CreatePlayerCommandHandler,CreatePlayerCommand,Player>
{
    private IContext _contextMock = Substitute.For<IContext>();

    protected override void SetupDependencies()
    {
        IQueryable<Player> mockedList = (new List<Player>()).AsQueryable();//BuildMock();
        _contextMock.QuerySet<Player>().Returns(call => mockedList);
    }

    protected override CreatePlayerCommand CreateValidCommand()
    {
        return new CreatePlayerCommand()
        {
            Name = "Alireza",
            Email = "Alireza@gmail.com",
            Password = "Aa123456!"
        };
    }

    protected override CreatePlayerCommandHandler CreateCommandHandler()
    {
        return new CreatePlayerCommandHandler(_contextMock);
    }

    [Test]
    public async Task Handle_CommandIsValid_ContextSaveChangesIsCalled()
    {
        await Handler!.Handle(Command!, new CancellationToken());
        
        await _contextMock
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}