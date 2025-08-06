using Yooresh.Domain.Entities.Players;
using Shouldly;
using NUnit.Framework;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.UnitTests.Domain.Entities.Accounts;

public class PlayerTests
{
    [Test]
    public void Constructor_SetPropertiesCorrectly()
    {
        var name = $"{Guid.NewGuid()}";
        var email = $"{Guid.NewGuid().ToString().ToUpper()}";
        var password = $"{Guid.NewGuid()}";

        Player player = new(name, email, password);

        player.Name.ShouldBe(name);
        player.Email.ShouldBe(email.ToLower());
        player.Password.ShouldBe(password);
        player.Role.ShouldBe(Role.SimplePlayer);
        player.RefreshTokens.ShouldBe([]);
    }

    [Test]
    public void Email_UpperCaseValue_ValueConvertedToLowerCase()
    {
        const string email = "MY EMAIL";
        Player player = new("name", email, "password");

        player.Email.ShouldBe(email.ToLower());
    }

    [Test]
    public void Constructor_ConfirmedShouldBeFalse()
    {
        Player player = new("name", "email", "password");

        player.Confirmed.ShouldBe(false);
    }

    [Test]
    public void Constructor_ConfirmationCodeShouldBeCreated()
    {
        Player player = new("name", "email", "password");

        string.IsNullOrWhiteSpace(player.ConfirmationCode).ShouldBeFalse();
        Guid.TryParse(player.ConfirmationCode, out _).ShouldBeTrue();
    }

    [Test]
    public void Constructor_IdShouldBeCreated()
    {
        Player player = new("name", "email", "password");

        player.Id.ShouldNotBe(Guid.Empty);
    }

    [Test]
    public void ConfirmPlayer_CorrectConfirmationCode_ShouldMakeThePlayerConfirmed()
    {
        Player player = new("name", "email", "password");

        player.ConfirmPlayer(player.ConfirmationCode);

        player.Confirmed.ShouldBe(true);
    }

    [Test]
    public void ConfirmPlayer_CorrectConfirmationCode_ShouldAddPlayerConfirmedEvent()
    {
        Player player = new("name", "email", "password");

        player.ConfirmPlayer(player.ConfirmationCode);

        player.DomainEvents.Any(a=> a is PlayerConfirmedEvent).ShouldBeTrue();
    }

    [Test]
    public void ConfirmPlayer_WrongConfirmationCode_ShouldNotMakeThePlayerConfirmed()
    {   
        Player player = new("name", "email", "password");

        player.ConfirmPlayer("wrong code");

        player.Confirmed.ShouldBe(false);
    }

    [Test]
    public void ConfirmPlayer_WrongConfirmationCode_ShouldNotAddPlayerConfirmedEvent()
    {
        Player player = new("name", "email", "password");

        player.ConfirmPlayer("Wrong Code");

        player.DomainEvents.Any(a => a is PlayerConfirmedEvent).ShouldBeFalse();
    }
}
