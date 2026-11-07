using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class ConfirmPlayerCommandHandlerTests:
    RequestHandlerTests<ConfirmAccountCommandHandler,ConfirmAccountCommand,bool>
{
    private IDocumentStore<Player>? _documentStore;
    private Player? _player;

    protected override Task InitDependenciesAsync()
    {
        _documentStore = Substitute.For<IDocumentStore<Player>>();
        _player = new Player("name", "my@email.com", "Aa123456");
        return Task.CompletedTask;
    }

    protected override Task SetupDependenciesAsync()
    {
        _documentStore!.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(_player);
        return Task.CompletedTask;
    }

    protected override ConfirmAccountCommand CreateValidRequest()
    {
        return new ConfirmAccountCommand()
        {
            PlayerId = _player!.Id,
            ConfirmationCode = _player.ConfirmationCode
        };
    }

    protected override ConfirmAccountCommandHandler CreateRequestHandler()
    {
        return new ConfirmAccountCommandHandler(_documentStore!);
    }

    [Test]
    public async Task Handle_CommandIsValid_PlayerIsConfirmedAndUpdated()
    {
        var result = await Handler!.Handle(Request!, default);

        result.ShouldBeTrue();
        _player!.Confirmed.ShouldBeTrue();
        await _documentStore!.Received(1)
            .UpdateAsync(_player, Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_ConfirmationCodeIsWrong_ReturnsFalseAndDoesNotUpdate()
    {
        Request!.ConfirmationCode = "wrong code";

        var result = await Handler!.Handle(Request!, default);

        result.ShouldBeFalse();
        _player!.Confirmed.ShouldBeFalse();
        await _documentStore!.DidNotReceive()
            .UpdateAsync(Arg.Any<Player>(), Arg.Any<CancellationToken>());
    }
}
