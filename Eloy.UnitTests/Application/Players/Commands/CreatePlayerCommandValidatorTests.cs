using Eloy.Application.Common.Interfaces;
using Eloy.Application.Players.Commands;
using Eloy.Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;   

namespace Eloy.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandValidatorTests
{
    private Mock<IContext>? _contextMock;
    private CreatePlayerCommand? _command;
    private CreatePlayerCommandValidator? _validator;

    public CreatePlayerCommandValidatorTests()
    {
        SetupDependencies();
    }

    private void SetupDependencies()
    {
        var data = new List<Player>().AsQueryable();
        
        _contextMock = new Mock<IContext>();
        _contextMock.Setup(x => x.Players)
            .ReturnsDbSet(data);
        
        _command = new CreatePlayerCommand()
        {
            Email = "a@gmail.com",
            Name = "PlayerName",
            Password = "123pass",
            PasswrodConfirmation = "123pass"
        };
        _validator = new CreatePlayerCommandValidator(_contextMock!.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("\r \t \n")]
    public async Task Validator_NameNotValid_ValidationShouldBeFalse(string name)
    {
        //Arrange
        _command!.Name = name;

        //Act
        var result = await _validator!.ValidateAsync(_command!);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains(nameof(CreatePlayerCommand.Name)));
    }
}