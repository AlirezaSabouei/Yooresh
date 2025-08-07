using MockQueryable;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Application.Accounts.Commands.Validators;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Players.Commands.Validators;

public class CreatePlayerCommandValidatorTests :
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
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains($"'{nameof(CreatePlayerCommand.Email)}' must not be empty."));
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
        result.Errors.ShouldContain(a => a.ErrorMessage.Contains($"'{nameof(CreatePlayerCommand.Email)}' is not a valid email address."));
    }

    [Test]
    [TestCase("test@test.com", "test@test.com")]
    [TestCase("TEST@TEST.COM", "test@test.com")]
    [TestCase("test@test.com", "TEST@TEST.COM")]
    [TestCase("Test@test.com", "test@Test.com")]
    public async Task Validator_EmailAlreadyExists_ValidationFalse(string existingEmail, string incomingEmail)
    {
        //Arrange
        var players = new List<Player>()
        {
            new("name", existingEmail,"password")
        }
        .BuildMock();
        _contextMock!.QuerySet<Player>().Returns(players.AsQueryable());
        Command!.Email = incomingEmail;

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
            $"'{nameof(CreatePlayerCommand.Password)}' should be at least 5 characters and contain both letters and numbers"));
    }
}