using Yooresh.Domain.Entities.Players;
using Shouldly;
using NUnit.Framework;

namespace Yooresh.UnitTests.Domain.Entities;

public class PlayerTests
{
    [Test]
    public void Email_UpperCaseValueSet_ValueConvertedToLowerCase()
    {
        const string email = "MY EMAIL";
        Player player = new("name", email, "password", Role.SimplePlayer);

        player.Email.ShouldBe(email.ToLower());
    }

    [Test]
    public void CreatedPlayer_ShouldHaveFalseConfirmed()
    {
        Player player = new("name", "email", "password", Role.SimplePlayer);
        player.Confirmed.ShouldBe(false);
    }

    [Test]
    public void ConfirmPlayer_ShouldMakeThePlayerConfirmed()
    {
        Player player = new("name", "email", "password", Role.SimplePlayer);

       // player.ConfirmPlayer();

        player.Confirmed.ShouldBe(true);
    }
}
