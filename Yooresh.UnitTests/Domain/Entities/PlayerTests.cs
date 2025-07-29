using Yooresh.Domain.Entities.Players;
using Shouldly;
using Xunit;

namespace Yooresh.UnitTests.Domain.Entities;

public class PlayerTests
{
    [Fact]
    public void Email_UpperCaseValueSet_ValueConvertedToLowerCase()
    {
        const string email = "MY EMAIL";
        Player player = new("name", email, "password", Role.SimplePlayer);

        player.Email.ShouldBe(email.ToLower());
    }

    [Fact]
    public void CreatedPlayer_ShouldHaveFalseConfirmed()
    {
        Player player = new("name", "email", "password", Role.SimplePlayer);
        player.Confirmed.ShouldBe(false);
    }

    [Fact]
    public void ConfirmPlayer_ShouldMakeThePlayerConfirmed()
    {
        Player player = new("name", "email", "password", Role.SimplePlayer);

       // player.ConfirmPlayer();

        player.Confirmed.ShouldBe(true);
    }
}
