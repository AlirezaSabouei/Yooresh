using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NUnit.Framework;
using Yooresh.AcceptanceTests.Drivers;
using Yooresh.API;
using Yooresh.Application.Common.Tools;

namespace Yooresh.AcceptanceTests.Controllers.Players;

public partial class PlayersControllerTests
{
    private PlayersDriver _playersDriver;
    private IDateTimeProvider _dateTimeProvider;

    public PlayersControllerTests()
    {
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var factory = new CustomYooreshWebApplicationFactory<Program>()
             .WithWebHostBuilder(builder =>
             {
                 builder.ConfigureServices(services =>
                 {
                     services.RemoveAll<IDateTimeProvider>();
                     services.AddSingleton(_dateTimeProvider);
                 });
             });
        _playersDriver = new PlayersDriver(factory);
    }

    [SetUp]
    protected async Task Setup()
    {
        await _playersDriver.ClearDatabaseAsync();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _playersDriver.Dispose();
    }
}
