using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.UnitTests.Domain.Events.Accounts;

public class PlayerCreatedEventTests
{
    [Test]
    public void Constructor_ShouldSetThePlayerProperty()
    {
        var player = new Player("name","email", "password");

        var playerCreatedEvent = new PlayerCreatedEvent(player);

        playerCreatedEvent.Player.ShouldBe(player);
    }
}
