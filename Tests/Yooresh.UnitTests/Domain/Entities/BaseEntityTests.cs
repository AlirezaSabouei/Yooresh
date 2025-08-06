using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Events;

namespace Yooresh.UnitTests.Domain.Entities;

public class BaseEntityTests
{
    private class TestEntity : BaseEntity { }
    private class TestEvent : BaseEvent { }

    [Test]
    public void Constructor_ShouldAssignNewGuid()
    {
        var baseEntity = new TestEntity();

        baseEntity.Id.ShouldNotBe(Guid.Empty);
    }

    [Test]
    public void AddDomainEvent_ShouldAddEvent()
    {
        var baseEntity = new TestEntity();
        var domainEvent = new TestEvent();

        baseEntity.AddDomainEvent(domainEvent);

        baseEntity.DomainEvents.Count.ShouldBe(1);
        baseEntity.DomainEvents.ShouldContain(domainEvent);
    }

    [Test]
    public void RemoveDomainEvent_ShouldRemoveEvent()
    {
        var baseEntity = new TestEntity();
        var domainEvent = new TestEvent();
        baseEntity.AddDomainEvent(domainEvent);

        baseEntity.RemoveDomainEvent(domainEvent);

        baseEntity.DomainEvents.Count.ShouldBe(0);
        baseEntity.DomainEvents.ShouldNotContain(domainEvent);
    }

    [Test]
    public void ClearDomainEvent_ShouldRemoveAllEvents()
    {
        var baseEntity = new TestEntity();
        var domainEvent1 = new TestEvent();
        var domainEvent2 = new TestEvent();
        baseEntity.AddDomainEvent(domainEvent1);
        baseEntity.AddDomainEvent(domainEvent2);

        baseEntity.ClearDomainEvents();

        baseEntity.DomainEvents.Count.ShouldBe(0);
    }
}
