using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Yooresh.Application.Players.Commands.Validators;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandValidatorTests:
    CommandValidatorTests<CreatePlayerCommandValidator,CreatePlayerCommand,Player>
{
    private Mock<IContext>? _contextMock;

    protected override void InitDependencies()
    {
        _contextMock = new Mock<IContext>();
    }

    protected override void SetupDependencies()
    {
        var dataPlayers = new List<Player>().AsQueryable();
        _contextMock!.Setup(x => x.Players)
            .ReturnsDbSet(dataPlayers);
    }

    protected override CreatePlayerCommand CreateValidCommand()
    {
        return new CreatePlayerCommand()
        {
            Email = "a@gmail.com",
            Name = "PlayerName",
            Password = "123pass"
        };
    }

    protected override CreatePlayerCommandValidator CreateCommandValidator()
    {
        return new CreatePlayerCommandValidator(_contextMock!.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("\r \t \n")]
    public async Task Validator_NameNotValid_ValidationShouldBeFalse(string name)
    {
        //Arrange
        Command!.Name = name;

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains(nameof(CreatePlayerCommand.Name)));
    }
}