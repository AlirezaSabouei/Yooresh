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
using Yooresh.Application.ResourceBuildings.Dto;
using Yooresh.Application.Villages.Commands;
using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;

namespace Yooresh.AcceptanceTests.Controllers.Villages;

public class VillagesControllerTests : ControllerTests<VillagesController>
{
    [Fact]
    public async Task CreateVillage_CommandIsValid_VillageIsCreated()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase();
        var dbFaction = await CreateAFactionInDatabase();
        var createVillageCommand = CreateValidCreateVillageCommand(dbPlayer,dbFaction);
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
        databaseVillage.Resource.ShouldBeEquivalentTo(new Resource(0,0,0,0,0));
        databaseVillage.Upgrades.Count.ShouldBe(0);
        databaseVillage.AvailableBuilders.ShouldBe(2);
        databaseVillage.ResourceBuildings.Count.ShouldBe(0);
    }
    
    private async Task<Player> CreateAPlayerInDatabase(bool confirmed=true)
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

    private CreateVillageCommand CreateValidCreateVillageCommand(Player player,Faction faction)
    {
        var createVillageCommand = new CreateVillageCommand()
        {
            Name = "Alireza",
            PlayerId = player.Id,
            FactionId = faction.Id
        };
        return createVillageCommand;
    }
    
    private void AddAuthenticationHeader(string email,string password)
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
            .Include(a=>a.Player)
            .Include(a=>a.Faction)
            .Include(a=>a.ResourceBuildings)
            .Include(a=>a.Upgrades)
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
        var dbPlayer =await CreateAPlayerInDatabase(confirmed:true);
        var dbFaction =await CreateAFactionInDatabase();
        var dbVillage =await CreateAVillageInDatabase(dbFaction,dbPlayer);
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri("api/villages", UriKind.Relative));
        var village = await result.Content.ReadFromJsonAsync<Village>();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        village.ShouldNotBeNull();
        village.FactionId.ShouldBe(dbFaction.Id);
        village.PlayerId.ShouldBe(dbPlayer.Id);
        village.Name.ShouldBe(dbVillage.Name);
        village.Resource.ShouldBeEquivalentTo(dbVillage.Resource);
        village.Upgrades.Count.ShouldBe(dbVillage.Upgrades.Count);
        village.AvailableBuilders.ShouldBe(dbVillage.AvailableBuilders);
        village.ResourceBuildings.Count.ShouldBe(dbVillage.ResourceBuildings.Count);
    }
    
    private async Task<Village> CreateAVillageInDatabase(Faction faction,Player player)
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var village = new Village("name", faction.Id, player.Id);
        context.Villages.Add(village);
        await context.SaveChangesAsync();
        return village;
    }
    
    [Fact]
    public async Task GetAvailableResourceBuildingUpgrades_UserNotAuthorized_ReturnsError()
    {
        //Act
        Client.DefaultRequestHeaders.Authorization = null;
        var result = await Client.GetAsync(new Uri("api/villages/AvailableResourceBuildingUpgrades", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetAvailableResourceBuildingUpgrades_UserIsValid_ReturnsVillage()
    {
        //Arrange
        var dbPlayer =await CreateAPlayerInDatabase(confirmed:true);
        var dbFaction =await CreateAFactionInDatabase();
        var dbVillage =await CreateAVillageInDatabase(dbFaction,dbPlayer);
        var dbBuilding=await CreateAResourceBuildingInDatabase();
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri("api/villages/AvailableResourceBuildingUpgrades", UriKind.Relative));

        var buildings = await result.Content.ReadFromJsonAsync<List<ResourceBuildingDto>>();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        buildings.ShouldNotBeNull();
        buildings.Count.ShouldBe(1);
        buildings[0].Name.ShouldBe(dbBuilding.Name);
    }

    private async Task<ResourceBuilding> CreateAResourceBuildingInDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var resourceBuilding = new ResourceBuilding(
            "Farm1", 
            new Resource(3600, 3600, 3600, 3600, 3600), 
            new Resource(0,0,0,0,0), 
            ProductionType.Food,
            new TimeSpan(0, 1, 0));
        context.ResourceBuildings.Add(resourceBuilding);
        await context.SaveChangesAsync();
        return resourceBuilding;
    }
    
    [Fact]
    public async Task UpgradeResourceBuilding_CommandIsValid_UpgradeAdded()
    {
        //Arrange
        var dbPlayer =await CreateAPlayerInDatabase(confirmed:true);
        var dbFaction =await CreateAFactionInDatabase();
        await CreateAVillageInDatabase(dbFaction,dbPlayer);
        var dbBuilding=await CreateAResourceBuildingInDatabase();
        var upgradeResourceBuildingCommandDto = new UpgradeResourceBuildingCommandDto()
        {
            ToId = dbBuilding.Id
        };
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);
        var request = BuildUpdateResourceBuildingRequest(upgradeResourceBuildingCommandDto);

        //Act
        var result = await Client.SendAsync(request);
        var databaseVillage = await GetCreatedVillageFromDatabase();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        databaseVillage.Upgrades.ShouldNotBeNull();
        databaseVillage.Upgrades.Count.ShouldBe(1);
        databaseVillage.Upgrades[0].Completed.ShouldBe(false);
        databaseVillage.Upgrades[0].FromId.ShouldBeNull();
        databaseVillage.Upgrades[0].ToId.ShouldBe(dbBuilding.Id);
        databaseVillage.Upgrades[0].UpgradeType.ShouldBe(UpgradeType.ResourceBuilding);
        
        databaseVillage.Upgrades[0].EndTime.Millisecond.ShouldBe((databaseVillage.Upgrades[0].StartTime+dbBuilding.UpgradeDuration).Millisecond);
    }
    
    private static HttpRequestMessage BuildUpdateResourceBuildingRequest(UpgradeResourceBuildingCommandDto upgradeResourceBuildingCommandDto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(upgradeResourceBuildingCommandDto),
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/villages/UpgradeResourceBuilding", UriKind.Relative)
        };
        return request;
    }
    
    //TODO: Validator should prevent 2 same class upgrades at the same time
    [Fact]
    public async Task UpgradeResourceBuilding_CommandIsValid_BuildingGoesToNextLevel()
    {
        //Arrange
        var dbPlayer =await CreateAPlayerInDatabase(confirmed:true);
        var dbFaction =await CreateAFactionInDatabase();
        await CreateAVillageInDatabase(dbFaction,dbPlayer);
        var dbBuilding1=await CreateAResourceBuildingInDatabase();
        var dbBuilding2=await CreateAResourceBuildingInDatabase();
        var upgradeResourceBuildingCommandDto = new UpgradeResourceBuildingCommandDto()
        {
            ToId = dbBuilding1.Id
        };
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);
        var request = BuildUpdateResourceBuildingRequest(upgradeResourceBuildingCommandDto);

        //Act
        await Client.SendAsync(request);
        
        upgradeResourceBuildingCommandDto = new UpgradeResourceBuildingCommandDto()
        {
            ToId = dbBuilding2.Id
        };
        request = BuildUpdateResourceBuildingRequest(upgradeResourceBuildingCommandDto);
        
        var result = await Client.SendAsync(request);
        
        var databaseVillage = await GetCreatedVillageFromDatabase();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        databaseVillage.Upgrades.ShouldNotBeNull();
        databaseVillage.Upgrades.Count.ShouldBe(2);
        databaseVillage.Upgrades[1].Completed.ShouldBe(false);
        databaseVillage.Upgrades[1].FromId.ShouldBe(dbBuilding1.Id);
        databaseVillage.Upgrades[1].ToId.ShouldBe(dbBuilding2.Id);
        databaseVillage.Upgrades[1].UpgradeType.ShouldBe(UpgradeType.ResourceBuilding);
        
        databaseVillage.Upgrades[1].EndTime.Millisecond.ShouldBe((databaseVillage.Upgrades[1].StartTime+dbBuilding2.UpgradeDuration).Millisecond);
    }
}