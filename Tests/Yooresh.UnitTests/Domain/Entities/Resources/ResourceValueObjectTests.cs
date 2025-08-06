using NUnit.Framework;
using Shouldly;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.UnitTests.Domain.Entities.Resources;

public class ResourceValueObjectTests
{
    [Test]
    public void Constructor_ShouldSetTheValues()
    {
        ResourceValueObject resourceValueObject =
            new(gold: 10, lumber: 20, stone: 30, food: 40);

        resourceValueObject.Gold.ShouldBe(10);
        resourceValueObject.Lumber.ShouldBe(20);
        resourceValueObject.Stone.ShouldBe(30);
        resourceValueObject.Food.ShouldBe(40);
    }

    [Test]
    public void PlusOperator_ShouldAddTheValues()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 10, lumber: 20, stone: 30, food: 40);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 2, stone: 3, food: 4);

        var result = resourceValueObject1 + resourceValueObject2;

        result.Gold.ShouldBe(11);
        result.Lumber.ShouldBe(22);
        result.Stone.ShouldBe(33);
        result.Food.ShouldBe(44);
    }

    [Test]
    public void EqualityOperator_ShouldCompareAllValues()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 2, lumber: 2, stone: 2, food: 2);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 3, food: 3);

        (resourceValueObject2<resourceValueObject1).ShouldBeTrue();
    }
}
