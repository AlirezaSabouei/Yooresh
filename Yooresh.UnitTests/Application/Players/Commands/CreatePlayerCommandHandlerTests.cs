using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.UnitTests.Application.Base;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Yooresh.UnitTests.Application.Players.Commands;

public class CreatePlayerCommandHandlerTests:
    CommandHandlerTests<CreatePlayerCommandHandler,CreatePlayerCommand,bool>
{
    private Mock<IEmail>? _emailMock;
    private Mock<IConfiguration>? _configurationMock;
    private Mock<IContext>? _contextMock;

    protected override void InitDependencies()
    {
        _emailMock = new Mock<IEmail>();
        _configurationMock = new Mock<IConfiguration>();
        _contextMock = new Mock<IContext>();
    }

    protected override void SetupDependencies()
    {
        _contextMock!.Setup(a => a.Players)
            .ReturnsDbSet((new List<Player>()).AsQueryable());

        _emailMock!.Setup(a => a.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() => { });

        _configurationMock!.Setup(a => a.GetSection(It.IsAny<string>())[It.IsAny<string>()])
            .Returns("Thank you for registering the game.\nPlease click the link below to activate your account:\n@link@guid");
    }

    protected override CreatePlayerCommand CreateValidCommand()
    {
        return new CreatePlayerCommand()
        {
            Name = "Alireza",
            Email = "Alireza@gmail.com",
            Password = "Aa123456",
            PasswordConfirmation = "Aa123456"
        };
    }

    protected override CreatePlayerCommandHandler CreateCommandHandler()
    {
        return new CreatePlayerCommandHandler(
            _contextMock!.Object,
            _emailMock!.Object,
            _configurationMock!.Object
        );
    }

    [Fact]
    public void Handle_CommandIsValid_ContextSaveChangesIsCalled()
    {
        Handler!.Handle(Command!, new CancellationToken());
        
        _contextMock!
            .Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
    }
    
    [Fact]
    public void Handle_CommandIsValid_EmailSendEmailIsCalled()
    {
        Handler!.Handle(Command!, new CancellationToken());
        
        _emailMock!
            .Verify(x=>x.SendEmail(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()),Times.Once);
    }
}