using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Events.Accounts;

namespace Yooresh.UnitTests.Domain.Events.Accounts;

public class PlayerConfirmedEventTests
{
    [Test]
    public void Constructor_ShouldSetThePlayerProperty()
    {
        var playerId = Guid.NewGuid();

        var playerConfirmedEvent = new PlayerConfirmedEvent(playerId);

        playerConfirmedEvent.PlayerId.ShouldBe(playerId);
    }
}
