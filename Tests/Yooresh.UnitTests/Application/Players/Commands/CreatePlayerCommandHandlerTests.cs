using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Account.Commands;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Events.Accounts;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandHandlerTests:
    RequestHandlerTests<CreatePlayerCommandHandler,CreatePlayerCommand,Player>
{
    private IContext? _contextMock;
    private IPasswordEncryption<BaseEntity>? _passwordEncryption;

    protected override Task InitDependenciesAsync()
    {
        _contextMock = Substitute.For<IContext>();
        _passwordEncryption = Substitute.For<IPasswordEncryption<BaseEntity>>();
        return Task.CompletedTask;
    }

    protected override Task SetupDependenciesAsync()
    {
        _passwordEncryption!.HashPassword(Arg.Any<Player>(), Arg.Any<string>())
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
        return new CreatePlayerCommandHandler(_contextMock!, _passwordEncryption!);
    }

    [Test]
    public async Task Handle_CommandIsValid_ContextSaveChangesIsCalled()
    {
        await Handler!.Handle(Request!, default);
        
        await _contextMock!
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
        await _contextMock!.Players
            .Received(1)
            .AddAsync(Arg.Any<Player>());
    }

    [Test]
    public async Task Handle_CommandIsValid_PasswordEncryptionIsCalled()
    {
        await Handler!.Handle(Request!, default);

        _passwordEncryption!
            .Received(1)
            .HashPassword(Arg.Is<Player>(a=>a.Name == Request!.Name), Arg.Is<string>(a => a == Request!.Password));
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
    public void Handle_WhenSaveFails_ThrowsException()
    {
        _contextMock!.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Throws(new Exception("Database failed"));

        Should.ThrowAsync<Exception>(() => Handler!.Handle(Request!, default));
    }
}