using MockQueryable;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Application.Players.Commands.Validators;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands.Validators;

public class CreatePlayerCommandValidatorTests:
    CommandValidatorTests<CreatePlayerCommandValidator, CreatePlayerCommand, Player>
{
    private IContext? _contextMock;

    protected override void InitDependencies()
    {
        _contextMock = Substitute.For<IContext>();
    }

    protected override void SetupDependencies()
    {
        var players = new List<Player>().BuildMock();
        _contextMock!.QuerySet<Player>().Returns(players.AsQueryable());
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
        return new CreatePlayerCommandValidator(_contextMock!);
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase("\r \t \n")]
    public async Task Validator_NameNotValid_ValidationFalse(string name)
    {
        //Arrange
        Command!.Name = name;

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains(nameof(CreatePlayerCommand.Name)));
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase("\r \t \n")]
    public async Task Validator_EmailEmpty_ValidationFalse(string email)
    {
        //Arrange
        Command!.Email = email;

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains($"'{nameof(CreatePlayerCommand.Email)}' should be provided"));
    }

    [Test]
    [TestCase("123")]
    [TestCase("adasd")]
    public async Task Validator_EmailNotValid_ValidationFalse(string email)
    {
        //Arrange
        Command!.Email = email;

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains($"'{nameof(CreatePlayerCommand.Email)}' should be a valid email address"));
    }

    [Test]
    public async Task Validator_EmailAlreadyExists_ValidationFalse()
    {
        //Arrange
        var email = "test@test.com".ToUpper();
        var players = new List<Player>() 
        { 
            new()
            { 
                Id = Guid.NewGuid(),
                Email = email
            }
        }.BuildMock();
        _contextMock!.QuerySet<Player>().Returns(players.AsQueryable());
        Command!.Email = email.ToLower();

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains($"'{nameof(CreatePlayerCommand.Email)}' is already in use"));
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase("\r \t \n")]
    [TestCase("ader")] //Less than 5 chars
    [TestCase("aderd")] //Having No Digit
    [TestCase("123")] //Less than 5 chars
    [TestCase("12345")] //Having no letters
    public async Task Validator_InvalidPassword_ValidationFalse(string password)
    {
        //Arrange
        Command!.Password = password;

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains(
            $"'{nameof(CreatePlayerCommand.Password)}' should be at least 5 chars consist of alphabet, Number and signs"));
    }
}