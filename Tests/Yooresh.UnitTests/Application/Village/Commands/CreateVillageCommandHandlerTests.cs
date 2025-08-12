using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Yooresh.Application.Common.Interfaces;
using Yooresh.UnitTests.Application.Base;

namespace Yooresh.UnitTests.Application.Village.Commands;

/*public class CreateVillageCommandHandlerTests:
    CommandHandlerTests<CreateVillageCommandHandler,CreateVillageCommand,bool>
{
    [Fact]
    public async Task Handle_ValidCommand_CreatesVillageAndResourceBuildings()
    {
        // Arrange
        var contextMock = new Mock<IContext>();
        var resourceBuildingMock = new Mock<DbSet<ResourceBuilding>>();

        contextMock.Setup(x => x.ResourceBuildings).Returns(resourceBuildingMock.Object);

        var handler = new CreateVillageCommandHandler(contextMock.Object);
        var command = new CreateVillageCommand
        {
            Name = "TestVillage",
            PlayerId = Guid.NewGuid(),
            FactionId = Guid.NewGuid()
        };

        // Assume that your DbSet<ResourceBuilding> has the necessary data for basic resource buildings.
        // You can set up your mock DbSet here.

        var village = new Village();

        // Assume you have a method to add resource buildings to the village.

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result); // Check if the result is true as expected.

        // Verify that AddBasicResourceBuildings method was called.
        // You can add more specific verifications if needed.
        // Example:
        contextMock.Verify(x => x.ResourceBuildings.SingleAsync(It.IsAny<Expression<Func<ResourceBuilding, bool>>>()), Times.Exactly(4));

        // Verify that the context's SaveChangesAsync method was called once.
        contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        // Verify any other expected behavior as needed.
    }
}*/