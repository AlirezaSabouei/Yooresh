using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Application.Accounts.Commands.Validators;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class ConfirmPlayerCommandValidatorTests:
    CommandValidatorTests<ConfirmPlayerCommandValidator,ConfirmAccountCommand,bool>
{
    private IDocumentStore<Player>? _documentStore;
    private Player? _player;

    protected override void InitDependencies()
    {
        _documentStore = Substitute.For<IDocumentStore<Player>>();
        _player = new Player("Alireza", "alireza@gmail.com", "Aa123456");
    }

    protected override void SetupDependencies()
    {
        _documentStore!.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(_player);
    }

    protected override ConfirmAccountCommand CreateValidCommand()
    {
        return new ConfirmAccountCommand()
        {
            PlayerId = _player!.Id,
            ConfirmationCode = Guid.NewGuid().ToString()
        };
    }

    protected override ConfirmPlayerCommandValidator CreateCommandValidator()
    {
        return new ConfirmPlayerCommandValidator(_documentStore!);
    }

    [Test]
    public async Task Validator_PlayerIdIsDefault_ValidationShouldBeFalse()
    {
        //Arrange
        Command!.PlayerId = new Guid();

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors
            .All(a => a.PropertyName.Contains(nameof(ConfirmAccountCommand.PlayerId)))
            .ShouldBeTrue();
    }

    [Test]
    public async Task Validator_PlayerIdNotExistsInDatabase_ValidationShouldBeFalse()
    {
        //Arrange
        _documentStore!.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Player?)null);
        Command!.PlayerId = Guid.NewGuid();

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors
            .All(a => a.PropertyName.Contains(nameof(Player)))
            .ShouldBeTrue();
    }
}
