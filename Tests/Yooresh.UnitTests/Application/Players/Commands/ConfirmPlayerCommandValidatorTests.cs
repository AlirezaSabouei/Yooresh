using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Yooresh.Application.Account.Commands;
using Yooresh.Application.Account.Commands.Validators;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class ConfirmPlayerCommandValidatorTests:CommandValidatorTests<ConfirmPlayerCommandValidator,ConfirmAccountCommand,bool>
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
            new("Alireza","alireza@gmail.com","Aa123456")
        }
        .AsQueryable();
        
        _contextMock!.Setup(x => x.Players)
            .ReturnsDbSet(dataPlayers);
    }

    protected override ConfirmAccountCommand CreateValidCommand()
    {
        return new ConfirmAccountCommand()
        {
            PlayerId = _contextMock!.Object.Players.First().Id,
            ConfirmationCode = Guid.NewGuid().ToString()
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
            .All(a => a.PropertyName.Contains(nameof(ConfirmAccountCommand.PlayerId)))
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