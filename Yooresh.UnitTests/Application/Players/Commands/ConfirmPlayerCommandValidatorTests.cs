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

public class ConfirmPlayerCommandValidatorTests:CommandValidatorTests<ConfirmPlayerCommandValidator,ConfirmPlayerCommand,bool>
{
    private Mock<IContext>? _contextMock;

    protected override void InitDependencies()
    {
        _contextMock = new Mock<IContext>();
    }

    protected override void SetupDependencies()
    {
        var dataPlayers = new List<Player>()
        {
            new Player("Alireza","alireza@gmail.com","Aa123456",Role.SimplePlayer)
        }.AsQueryable();
        
        _contextMock!.Setup(x => x.Players)
            .ReturnsDbSet(dataPlayers);
    }

    protected override ConfirmPlayerCommand CreateValidCommand()
    {
        return new ConfirmPlayerCommand()
        {
            PlayerId = _contextMock!.Object.Players.First().Id
        };
    }

    protected override ConfirmPlayerCommandValidator CreateCommandValidator()
    {
        return new ConfirmPlayerCommandValidator(_contextMock!.Object);
    }
    
    [Fact]
    public async Task Validator_PlayerIdIsDefault_ValidationShouldBeFalse()
    {
        //Arrange
        Command!.PlayerId = new Guid();

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors
            .All(a => a.PropertyName.Contains(nameof(ConfirmPlayerCommand.PlayerId)))
            .ShouldBeTrue();
    }
    
    [Fact]
    public async Task Validator_PlayerIdNotExistsInDatabase_ValidationShouldBeFalse()
    {
        //Arrange
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