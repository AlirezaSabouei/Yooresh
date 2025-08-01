using NUnit.Framework;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Yooresh.AcceptanceTests.Drivers;
using Yooresh.API;
using Yooresh.Application.Resources.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yooresh.Application.Common.Tools;
using NSubstitute;

namespace Yooresh.AcceptanceTests.Controllers;

public class ResourcesControllerTests
{
    private ResourcesDriver _resourcesDriver;
    private IDateTimeProvider _dateTimeProvider;

    public ResourcesControllerTests()
    {
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var factory = new CustomYooreshWebApplicationFactory<Program>()
             .WithWebHostBuilder(builder =>
             {
                 builder.ConfigureServices(services =>
                 {
                     services.RemoveAll<IDateTimeProvider>();
                     services.AddSingleton<IDateTimeProvider>(_dateTimeProvider);
                 });
             });
        _resourcesDriver = new ResourcesDriver(factory);
    }

    [SetUp]
    protected async Task Setup()
    {
        await _resourcesDriver.ClearDatabaseAsync();
    }

    [Test]
    public async Task GetResources_QueryIsValid_ResourcesAreReturned()
    {
        //Arrange
        var resource1 = _resourcesDriver.CreateEntityInstance();
        var resource2 = _resourcesDriver.CreateEntityInstance();
        var resource3 = _resourcesDriver.CreateEntityInstance();
        resource3.Player.Id = resource2.Player.Id = resource1.Player.Id = FakeAuthenticationHandler.PlayerId;
        await _resourcesDriver.AddEntityToDatabaseAsync(resource1);
        await _resourcesDriver.AddEntityToDatabaseAsync(resource2);
        await _resourcesDriver.AddEntityToDatabaseAsync(resource3);

        //Act
        var result = await _resourcesDriver.HttpClient.GetAsync($"api/resources");

        //Assert
        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.Content.ShouldNotBeNull();
        (await result.Content.ReadFromJsonAsync<List<ResourceDto>>())!.Count.ShouldBe(3);
    }

    [Test]
    public async Task GetResources_CalculatedResourceAmountIsCorrect()
    {
        //Arrange
        var dateTime = DateTime.UtcNow;
        _dateTimeProvider.GetUtcNow().Returns(dateTime);
        var resource = _resourcesDriver.CreateEntityInstance();
        resource.AvailableAmount = 0;
        resource.HarvestRatePerMinute = 10;
        resource.LastUpdateDate = dateTime.AddMinutes(-10);
        resource.Player.Id = FakeAuthenticationHandler.PlayerId;
        await _resourcesDriver.AddEntityToDatabaseAsync(resource);

        //Act
        var result = await _resourcesDriver.HttpClient.GetAsync($"api/resources");

        //Assert
        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.Content.ShouldNotBeNull();
        (await result.Content.ReadFromJsonAsync<List<ResourceDto>>())!.First().AvailableAmount.ShouldBe(100);
    }
}