using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Account;
using Yooresh.Infrastructure.PasswordTools;

namespace Yooresh.UnitTests.Infrustructure.PasswordTools;

public class PasswordEncryptionTests
{
    private IPasswordHasher<BaseEntity>? _passwordHasherMock;

    private string _hashedPassword = Guid.NewGuid().ToString();

    [SetUp]
    public Task SetupDependencies()
    {
        _passwordHasherMock = Substitute.For<IPasswordHasher<BaseEntity>>();
        _passwordHasherMock.HashPassword(Arg.Any<BaseEntity>(), Arg.Any<string>())
            .Returns(_hashedPassword);
        _passwordHasherMock.VerifyHashedPassword(Arg.Any<BaseEntity>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(PasswordVerificationResult.Success);
        return Task.CompletedTask;
    }

    [Test]
    public Task HashPassword_ValidInput_ReturnsHashedPassword()
    {
        var passwordEncryption = new PasswordEncryption<BaseEntity>(_passwordHasherMock!);
        var player = CreateAPlayer();

        player.Password = passwordEncryption.HashPassword(player, player.Password);
        
        player.Password.ShouldBe(_hashedPassword);
        return Task.CompletedTask;
    }

    private Player CreateAPlayer()
    {
        return new Player(
            name: "name",
            email: "alireza.sabouei@gmail.com",
            password: "Aa123456");
    }

    [Test]
    public Task HashPassword_ValidInput_PaswordHasherIsCalled()
    {
        var passwordEncryption = new PasswordEncryption<BaseEntity>(_passwordHasherMock!);
        var player = CreateAPlayer();

        passwordEncryption.HashPassword(player, player.Password);

        _passwordHasherMock!.Received(1)
            .HashPassword(player, player.Password);
        return Task.CompletedTask;
    }

    [Test]
    public Task VerifyPassword_ValidInput_PaswordHasherIsCalled()
    {
        var passwordEncryption = new PasswordEncryption<BaseEntity>(_passwordHasherMock!);
        var player = CreateAPlayer();

        passwordEncryption.VerifyPassword(player, _hashedPassword, player.Password);

        _passwordHasherMock!.Received(1)
            .VerifyHashedPassword(player, _hashedPassword, player.Password);
        return Task.CompletedTask;
    }

    [Test]
    public Task VerifyPassword_ValidInput_ReturnsTrue()
    {
        var passwordEncryption = new PasswordEncryption<BaseEntity>(_passwordHasherMock!);
        var player = CreateAPlayer();

        var result = passwordEncryption.VerifyPassword(player, _hashedPassword, player.Password);

        result.ShouldBeTrue(); 
        return Task.CompletedTask;
    }

    [Test]
    public Task VerifyPassword_InvalidInput_ReturnsFalse()
    {
        _passwordHasherMock!.VerifyHashedPassword(Arg.Any<BaseEntity>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(PasswordVerificationResult.Failed);
        var passwordEncryption = new PasswordEncryption<BaseEntity>(_passwordHasherMock);
        var player = CreateAPlayer();

        var result = passwordEncryption.VerifyPassword(player, _hashedPassword, player.Password);

        result.ShouldBeFalse();
        return Task.CompletedTask;
    }
}
