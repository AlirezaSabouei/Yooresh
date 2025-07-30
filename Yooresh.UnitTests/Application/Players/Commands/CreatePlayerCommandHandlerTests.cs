using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Events;
using NSubstitute.ReceivedExtensions;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandHandlerTests:
    RequestHandlerTests<CreatePlayerCommandHandler,CreatePlayerCommand,Player>
{
    private IContext _contextMock = Substitute.For<IContext>();

    protected override void SetupDependencies()
    {
        
    }

    protected override CreatePlayerCommand CreateValidRequest()
    {
        return new CreatePlayerCommand()
        {
            Name = "Alireza",
            Email = "Alireza@gmail.com",
            Password = "Aa123456!"
        };
    }

    protected override CreatePlayerCommandHandler CreateRequestHandler()
    {
        return new CreatePlayerCommandHandler(_contextMock);
    }

    [Test]
    public async Task Handle_CommandIsValid_ContextSaveChangesIsCalled()
    {
        await Handler!.Handle(Request!, new CancellationToken());
        
        await _contextMock
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
        await _contextMock.Players
            .Received(1)
            .AddAsync(Arg.Any<Player>());
    }

    [Test]
    public async Task Handle_CommandIsValid_PlayerIsCreated()
    {
        var player = await Handler!.Handle(Request!, new CancellationToken());

        player.Name.ShouldBe(Request.Name);
        player.Email.ShouldBe(Request.Email.ToLower());
        player.Password.ShouldBe(Request.Password);
        player.Confirmed.ShouldBeFalse();
        (Convert.ToInt32(player.ConfirmationCode)).ShouldBeGreaterThanOrEqualTo(12345678);
        player.Role.ShouldBe(Role.SimplePlayer);
        player.DomainEvents.Count(a => a is PlayerCreatedEvent).ShouldBe(1);        
    }
}