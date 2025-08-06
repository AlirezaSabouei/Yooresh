using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Entities.Account;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.UnitTests.Domain.Entities.Resources;

public class ResourceTests
{
    [Test]
    public void SettingProperties_ShouldWork()
    {
        var id = Guid.NewGuid();
        PlayerReference playerReference=new() { Id = Guid.NewGuid() };
        DateTime lastUpdatedDate = DateTime.UtcNow;

        Resource resource = new()
        {
            Id = id,
            Player = playerReference,
            ResourceType = ResourceType.Food,
            AvailableAmount = 50,
            HarvestRatePerMinute = 40,
            LastUpdateDate = lastUpdatedDate
        };

        resource.Id.ShouldBe(id);
        resource.Player.ShouldBe(playerReference);
        resource.ResourceType.ShouldBe(ResourceType.Food);
        resource.AvailableAmount.ShouldBe(50);
        resource.HarvestRatePerMinute.ShouldBe(40);
        resource.LastUpdateDate.ShouldBe(lastUpdatedDate);
    }

    [Test]
    public void CalculateAvailableAmount_ValidDateTime_CalculatesCorrectAmount()
    {
        var date = DateTime.Now;
        var resource = new Resource()
        {
            Player = new PlayerReference(),
            AvailableAmount = 100,
            HarvestRatePerMinute = 52,
            LastUpdateDate = date.AddMinutes(-10)           
        };

        resource.CalculateAvailableAmount(date);

        resource.AvailableAmount.ShouldBe(620);
    }

    [Test]
    public void CalculateAvailableAmount_DateTimeOlderThanLastUpdate_CalculatesCorrectAmount()
    {
        var date = DateTime.Now;
        var resource = new Resource()
        {
            Player = new PlayerReference(),
            AvailableAmount = 100,
            LastUpdateDate = date.AddMinutes(+10)
        };

        resource.CalculateAvailableAmount(date);

        resource.AvailableAmount.ShouldBe(100);
    }

    [Test]
    public void CalculateAvailableAmount_DateTimeEqualToLastUpdate_CalculatesCorrectAmount()
    {
        var date = DateTime.Now;
        var resource = new Resource()
        {
            Player = new PlayerReference(),
            AvailableAmount = 100,
            LastUpdateDate = date
        };

        resource.CalculateAvailableAmount(date);

        resource.AvailableAmount.ShouldBe(100);
    }
}
