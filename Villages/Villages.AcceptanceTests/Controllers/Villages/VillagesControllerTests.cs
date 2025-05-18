using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Yooresh.API.Controllers;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Villages.Commands;
using Yooresh.Application.Villages.Dto;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.Enums;
using Yooresh.Domain.Players;
using Yooresh.Domain.ValueObjects;
using Village = Yooresh.Domain.Entities.Villages.Village;

namespace Yooresh.AcceptanceTests.Controllers.Villages;

public class VillagesControllerTests : ControllerTests<VillagesController>
{
    [Fact]
    public async Task CreateVillage_CommandIsValid_VillageIsCreated()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase();
        var dbFaction = await CreateAFactionInDatabase();
        await CreateBasicResourceBuildingsInDatabase();
        var createVillageCommand = CreateValidCreateVillageCommand(dbPlayer, dbFaction);
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);
        var request = BuildCreateVillageRequest(createVillageCommand);

        //Act
        var result = await Client.SendAsync(request);
        var databaseVillage = await GetCreatedVillageFromDatabase();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        databaseVillage.ShouldNotBeNull();
        databaseVillage.Faction.Id.ShouldBe(dbFaction.Id);
        databaseVillage.Player.Id.ShouldBe(dbPlayer.Id);
        databaseVillage.Name.ShouldBe(createVillageCommand.Name);
        databaseVillage.Resource.ShouldBeEquivalentTo(new Resource(0, 0, 0, 0, 0));
        databaseVillage.AvailableBuilders.ShouldBe(2);
        databaseVillage.VillageResourceBuildings.Count.ShouldBe(0);
    }

    private async Task<Player> CreateAPlayerInDatabase(bool confirmed = true)
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var player = new Player("alireza", "alireza@gmail.com", "Aa123456", Role.SimplePlayer);
        if (confirmed)
        {
            player.ConfirmPlayer();
        }

        context.Players.Add(player);
        await context.SaveChangesAsync();
        return player;
    }

    private async Task<Faction> CreateAFactionInDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var faction = new Faction("testFaction", new[] {"advantage1"}, new[] {"diadvantage1"});
        context.Factions.Add(faction);
        await context.SaveChangesAsync();
        return faction;
    }

    private async Task CreateBasicResourceBuildingsInDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();

        var farm = new Farm()
        {
            Id = Guid.NewGuid(),
            UpgradeDuration = new TimeSpan(0, 0, 0),
            HourlyProduction = new Resource(0, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            TargetId = null,
            Level = 0
        };
        context.Buildings.Add(farm);

        var lumberMill = new LumberMill()
        {
            Id = Guid.NewGuid(),
            UpgradeDuration = new TimeSpan(0, 0, 0),
            HourlyProduction = new Resource(0, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            TargetId = null,
            Level = 0
        };
        context.Buildings.Add(lumberMill);

        var stoneMine = new StoneMine()
        {
            Id = Guid.NewGuid(),
            UpgradeDuration = new TimeSpan(0, 0, 0),
            HourlyProduction = new Resource(0, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            TargetId = null,
            Level = 0
        };
        context.Buildings.Add(stoneMine);

        var metalMine = new MetalMine()
        {
            Id = Guid.NewGuid(),
            UpgradeDuration = new TimeSpan(0, 0, 0),
            HourlyProduction = new Resource(0, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            TargetId = null,
            Level = 0
        };
        context.Buildings.Add(metalMine);

        await context.SaveChangesAsync();
    }

    private CreateVillageCommand CreateValidCreateVillageCommand(Player player, Faction faction)
    {
        var createVillageCommand = new CreateVillageCommand()
        {
            Name = "Alireza",
            PlayerId = player.Id,
            FactionId = faction.Id
        };
        return createVillageCommand;
    }

    private void AddAuthenticationHeader(string email, string password)
    {
        var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{password}"));
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
    }

    private static HttpRequestMessage BuildCreateVillageRequest(CreateVillageCommand createVillageCommand)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(createVillageCommand),
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/villages", UriKind.Relative)
        };
        return request;
    }

    private async Task<Village> GetCreatedVillageFromDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var village = await context.Villages
            .AsNoTracking()
            .FirstAsync();
        return village;
    }

    protected override async Task ClearDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        context.Players.RemoveRange(context.Players);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateVillage_UserNotAuthorized_ReturnsError()
    {
        //Act
        Client.DefaultRequestHeaders.Authorization = null;
        var request = BuildCreateVillageRequest(new CreateVillageCommand());
        var result = await Client.SendAsync(request);

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetVillage_UserNotAuthorized_ReturnsError()
    {
        //Act
        Client.DefaultRequestHeaders.Authorization = null;
        var result = await Client.GetAsync(new Uri("api/villages", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetVillage_UserIsValid_ReturnsVillage()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase(confirmed: true);
        var dbFaction = await CreateAFactionInDatabase();
        var dbVillage = await CreateAVillageInDatabase(dbFaction, dbPlayer);
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri("api/villages", UriKind.Relative));
        var village = await result.Content.ReadFromJsonAsync<VillageDto>();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        village.ShouldNotBeNull();
        village.FactionId.ShouldBe(dbFaction.Id);
        village.PlayerId.ShouldBe(dbPlayer.Id);
        village.Name.ShouldBe(dbVillage.Name);
        village.Resource.ShouldBeEquivalentTo(dbVillage.Resource);
        village.AvailableBuilders.ShouldBe(dbVillage.AvailableBuilders);
       // village.ResourceBuildings.Count.ShouldBe(dbVillage.VillageResourceBuildings.Count);
    }

    private async Task<Village> CreateAVillageInDatabase(Faction faction, Player player,
        bool initStartingResource = false)
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var village = new Village()
        {
            Name = "name",
            FactionId = faction.Id,
            PlayerId = player.Id
        };
        if (initStartingResource)
        {
            village.Resource = new Resource(100, 100, 100, 100, 100);
        }

        context.Villages.Add(village);
        await context.SaveChangesAsync();
        return village;
    }

    private async Task<Building> CreateAResourceBuildingInDatabase(Guid villageId)
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var village = context.Villages.Include(a => a.VillageResourceBuildings).First();
        var resourceBuilding1 = new Farm()
        {
            Id = new Guid("769a5118-f48b-4e55-b5fd-616d22a357b0"),
            UpgradeDuration = new TimeSpan(0, 2, 0),
            HourlyProduction = new Resource(1200, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            Level = 1,
            TargetId = new Guid("8e29536e-a9f9-4edb-ab95-9974e969887c")
        };
        
        var resourceBuilding2 = new Farm()
        {
            Id = new Guid("8e29536e-a9f9-4edb-ab95-9974e969887c"),
            UpgradeDuration = new TimeSpan(0, 2, 0),
            HourlyProduction = new Resource(2200, 0, 0, 0, 0),
            UpgradeCost = new Resource(0, 0, 0, 0, 0),
            Level = 2,
            TargetId = Guid.NewGuid()
        };
        context.Buildings.Add(resourceBuilding1);        
        context.Buildings.Add(resourceBuilding2);
        await context.SaveChangesAsync();
        village.VillageResourceBuildings.Add(new VillageBuilding(village, resourceBuilding1));
        await context.SaveChangesAsync();
        return resourceBuilding1;
    }
    
    [Fact]
    public async Task UpgradeResourceBuilding_CommandIsValid_BuildingGoesToNextLevel()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase(confirmed: true);
        var dbFaction = await CreateAFactionInDatabase();
        var village = await CreateAVillageInDatabase(dbFaction, dbPlayer);
        var dbBuilding = await CreateAResourceBuildingInDatabase(village.Id);
        var upgradeResourceBuildingCommandDto = new UpgradeResourceBuildingCommandDto()
        {
            ResourceBuildingId = dbBuilding.Id
        };
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);
        var request = BuildUpdateResourceBuildingRequest(upgradeResourceBuildingCommandDto);

        //Act
        upgradeResourceBuildingCommandDto = new UpgradeResourceBuildingCommandDto()
        {
            ResourceBuildingId = dbBuilding.Id
        };
        request = BuildUpdateResourceBuildingRequest(upgradeResourceBuildingCommandDto);

        var result = await Client.SendAsync(request);

        var databaseVillage = await GetCreatedVillageFromDatabase();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        databaseVillage.VillageResourceBuildings.Count(a => a.Id == dbBuilding.TargetId).ShouldBe(1);
    }

    private static HttpRequestMessage BuildUpdateResourceBuildingRequest(
        UpgradeResourceBuildingCommandDto upgradeResourceBuildingCommandDto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(upgradeResourceBuildingCommandDto),
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/villages/UpgradeResourceBuilding", UriKind.Relative)
        };
        return request;
    }
}