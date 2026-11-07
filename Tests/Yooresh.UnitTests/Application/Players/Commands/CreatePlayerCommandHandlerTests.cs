using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Events.Accounts;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandHandlerTests:
    RequestHandlerTests<CreatePlayerCommandHandler,CreatePlayerCommand,Player>
{
    private IDocumentStore<Player>? _documentStore;
    private IPasswordEncryption<BaseEntity>? _passwordEncryption;

    protected override Task InitDependenciesAsync()
    {
        _documentStore = Substitute.For<IDocumentStore<Player>>();
        _passwordEncryption = Substitute.For<IPasswordEncryption<BaseEntity>>();
        return Task.CompletedTask;
    }

    protected override Task SetupDependenciesAsync()
    {
        _passwordEncryption!.HashPassword(Arg.Any<BaseEntity>(), Arg.Any<string>())
            .Returns("HashedPassword");
        return Task.CompletedTask;
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
        return new CreatePlayerCommandHandler(_documentStore!, _passwordEncryption!);
    }

    [Test]
    public async Task Handle_CommandIsValid_PlayerIsInserted()
    {
        await Handler!.Handle(Request!, default);

        await _documentStore!
            .Received(1)
            .InsertAsync(Arg.Any<Player>(), Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_CommandIsValid_PasswordEncryptionIsCalled()
    {
        await Handler!.Handle(Request!, default);

        _passwordEncryption!
            .Received(1)
            .HashPassword(Arg.Is<BaseEntity>(a=>a is Player && ((Player)a).Name == Request!.Name),
                Arg.Is<string>(a => a == Request!.Password));
    }

    [Test]
    public async Task Handle_CommandIsValid_PlayerIsCreated()
    {
        var player = await Handler!.Handle(Request!, default);

        player.Name.ShouldBe(Request!.Name);
        player.Confirmed.ShouldBeFalse();
        Guid.TryParse(player.ConfirmationCode, out Guid confirmationCode).ShouldBeTrue();
        player.Role.ShouldBe(Role.SimplePlayer);
        player.DomainEvents.Count(a => a is PlayerCreatedEvent).ShouldBe(1);
    }

    [Test]
    public async Task Handle_PlayerCreated_EmailIsNormalized()
    {
        Request!.Email = "  TEST@EXAMPLE.COM  ";

        var player = await Handler!.Handle(Request!, default);

        player.Email.ShouldBe("test@example.com");
    }

    [Test]
    public async Task Handle_PlayerCreated_PasswordIsEncrypted()
    {
        var player = await Handler!.Handle(Request!, default);

        player.Password.ShouldBe("HashedPassword");
        player.Password.ShouldNotBe(Request!.Password);
    }

    [Test]
    public async Task Handle_WhenInsertFails_ThrowsException()
    {
        _documentStore!.InsertAsync(Arg.Any<Player>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException(new Exception("Database failed")));

        await Should.ThrowAsync<Exception>(() => Handler!.Handle(Request!, default));
    }
}
