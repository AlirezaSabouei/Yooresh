using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Events;

namespace Yooresh.UnitTests.Domain.Entities;

public class BaseEntityReferenceTests
{
    private class TestEntityReference : BaseEntityReference { }

    [Test]
    public void Constructor_ShouldAssignNewGuid()
    {
        var entityReference = new TestEntityReference();

        entityReference.Id.ShouldNotBe(Guid.Empty);
    }
}
