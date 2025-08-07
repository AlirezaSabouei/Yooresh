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
    public void MultipleOperator_ShouldMultipleTheValues()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 10, lumber: 20, stone: 30, food: 40);

        var result = resourceValueObject1 * 3;

        result.Gold.ShouldBe(30);
        result.Lumber.ShouldBe(60);
        result.Stone.ShouldBe(90);
        result.Food.ShouldBe(120);
    }

    [Test]
    public void ComparingOperator_AllValuesAreEqual_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 2, stone: 3, food: 4);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 2, stone: 3, food: 4);

        (resourceValueObject2 < resourceValueObject1).ShouldBeFalse();
        (resourceValueObject2 > resourceValueObject1).ShouldBeFalse();
    }

    [Test]
    public void SmallerOperator_AllValuesAreSmaller_ShouldBeTru()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 2, lumber: 3, stone: 4, food: 5);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 2, stone: 3, food: 4);

        (resourceValueObject2 < resourceValueObject1).ShouldBeTrue();
    }

    [Test]
    public void SmallerOperator_SomeValuesAreSmaller_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 2, lumber: 3, stone: 4, food: 5);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 4, stone: 6, food: 4);

        (resourceValueObject2 < resourceValueObject1).ShouldBeTrue();
    }

    [Test]
    public void BiggerOperator_AllValuesAreBigger_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 3, food: 3);
        ResourceValueObject resourceValueObject2 =
            new(gold: 2, lumber: 4, stone: 4, food: 5);

        (resourceValueObject2 > resourceValueObject1).ShouldBeTrue();
    }

    [Test]
    public void BiggerOperator_SomeValuesAreBigger_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 3, food: 3);
        ResourceValueObject resourceValueObject2 =
            new(gold: 2, lumber: 3, stone: 2, food: 1);

        (resourceValueObject2 > resourceValueObject1).ShouldBeFalse();
    }

    [Test]
    public void Equals_OneObjectIsNull_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject? resourceValueObject2 = null;

        resourceValueObject1.Equals(resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void Equals_ObjectOfDifferentType_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        string resourceValueObject2 = "Some string";

        resourceValueObject1.Equals(resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void Equals_AllValuesAreTheSame_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        resourceValueObject1.Equals(resourceValueObject2).ShouldBeTrue();
    }

    [Test]
    public void Equals_AllValuesAreDifferent_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 3, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        resourceValueObject1.Equals(resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void Equals_SomeValuesAreTheSame_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 5, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 7, lumber: 3, stone: 5, food: 7);

        resourceValueObject1.Equals(resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void EqualityOperator_OneObjectIsNull_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject? resourceValueObject2 = null;

        (resourceValueObject1 == resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void EqualityOperator_AllValuesAreTheSame_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 == resourceValueObject2).ShouldBeTrue();
    }

    [Test]
    public void EqualityOperator_AllValuesAreDifferent_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 3, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 == resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void EqualityOperator_SomeValuesAreTheSame_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 5, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 7, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 == resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void InequalityOperator_OneObjectIsNull_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject? resourceValueObject2 = null;

        (resourceValueObject1 != resourceValueObject2).ShouldBeTrue();
    }

    [Test]
    public void InequalityOperator_AllValuesAreTheSame_ShouldBeFalse()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 != resourceValueObject2).ShouldBeFalse();
    }

    [Test]
    public void InequalityOperator_AllValuesAreDifferent_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 3, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 1, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 != resourceValueObject2).ShouldBeTrue();
    }

    [Test]
    public void InequalityOperator_SomeValuesAreTheSame_ShouldBeTrue()
    {
        ResourceValueObject resourceValueObject1 =
            new(gold: 7, lumber: 5, stone: 5, food: 1);
        ResourceValueObject resourceValueObject2 =
            new(gold: 7, lumber: 3, stone: 5, food: 7);

        (resourceValueObject1 != resourceValueObject2).ShouldBeTrue();
    }
}
