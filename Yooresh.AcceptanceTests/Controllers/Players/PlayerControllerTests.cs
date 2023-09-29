using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Yooresh.API.Controllers;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Players.Commands;
using Yooresh.Domain.Entities.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using IServiceScopeFactory = Microsoft.Extensions.DependencyInjection.IServiceScopeFactory;

namespace Yooresh.AcceptanceTests.Controllers.Players;

public class PlayersControllerTests : ControllerTests<PlayersController>
{
    [Fact]
    public async Task CreatePlayer_CommandIsValid_PlayerIsCreated()
    {
        //Arrange
        var createPlayerCommand = CreateValidCreatePlayerCommand();
        var request = BuildCreatePlayerRequest(createPlayerCommand);

        //Act
        var result = await Client.SendAsync(request);
        var databasePlayer = await GetCreatedPlayerFromDatabase();

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        databasePlayer.ShouldNotBeNull();
        databasePlayer.Email.ShouldBe(createPlayerCommand.Email.ToLower());
    }

    private CreatePlayerCommand CreateValidCreatePlayerCommand()
    {
        var createPlayerCommand = new CreatePlayerCommand()
        {
            Name = "Alireza",
            Email = "Alireza@gmail.com",
            Password = "Aa123456",
            PasswordConfirmation = "Aa123456"
        };
        return createPlayerCommand;
    }

    private static HttpRequestMessage BuildCreatePlayerRequest(CreatePlayerCommand createPlayerCommand)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(createPlayerCommand),
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/players", UriKind.Relative)
        };
        return request;
    }

    private async Task<Player> GetCreatedPlayerFromDatabase()
    {
        var scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();
        var player = await context.Players
            .AsNoTracking()
            .FirstAsync();
        return player;
    }

    [Fact]
    public async Task CreatePlayer_CreatedPlayer_ShouldNotBeConfirmed()
    {
        //Arrange
        var createPlayerCommand = CreateValidCreatePlayerCommand();

        var request = BuildCreatePlayerRequest(createPlayerCommand);

        //Act
        await Client.SendAsync(request);
        var databasePlayer = await GetCreatedPlayerFromDatabase();

        //Assert
        databasePlayer.Confirmed.ShouldBeFalse();
    }

    [Fact]
    public async Task CreatePlayer_CreatedPlayer_ShouldBeSimplePlayer()
    {
        //Arrange
        var createPlayerCommand = CreateValidCreatePlayerCommand();

        var request = BuildCreatePlayerRequest(createPlayerCommand);

        //Act
        await Client.SendAsync(request);
        var databasePlayer = await GetCreatedPlayerFromDatabase();

        //Assert
        databasePlayer.Role.ShouldBe(Role.SimplePlayer);
    }
    
    [Fact]
    public async Task CreatePlayer_CreatedPlayer_EmailShouldBeStoredInLowerCase()
    {
        //Arrange
        var createPlayerCommand = CreateValidCreatePlayerCommand();
        createPlayerCommand.Email = createPlayerCommand.Email.ToUpper();

        var request = BuildCreatePlayerRequest(createPlayerCommand);

        //Act
        await Client.SendAsync(request);
        var databasePlayer = await GetCreatedPlayerFromDatabase();

        //Assert
        databasePlayer.Email.ShouldBe(createPlayerCommand.Email.ToLower());
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
    public async Task GetPlayer_UserNotAuthorized_ReturnsError()
    {
        //Act
        var result = await Client.GetAsync(new Uri("api/players", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetPlayer_ValidEmailAndPassword_ShouldReturnPlayer()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase();
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri("api/players", UriKind.Relative));
        var player = await result.Content.ReadFromJsonAsync<Player>();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        player!.Email.ShouldBe(dbPlayer.Email);
        player.Role.ShouldBe(Role.SimplePlayer);
    }
    
    [Fact]
    public async Task GetPlayer_ValidEmailAndPasswordAndEmailIsUppercase_ShouldReturnPlayer()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase();
        AddAuthenticationHeader(dbPlayer.Email,dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri("api/players", UriKind.Relative));
        var player = await result.Content.ReadFromJsonAsync<Player>();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        player!.Email.ShouldBe(dbPlayer.Email.ToLower());
        player.Role.ShouldBe(Role.SimplePlayer);
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
    
    private void AddAuthenticationHeader(string email,string password)
    {
        var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{password}"));
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
    }
    
    [Fact]
    public async Task ConfirmPlayer_PlayerIdNotValid_ExceptionIsReturned()
    {
        //Act
        var result = await Client.GetAsync(new Uri($"api/confirmPlayer?playerid={new Guid()}", UriKind.Relative));
        var message =(await result.Content.ReadAsStringAsync()).ToLower();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        message.ShouldContain("validation failed");
        message.ShouldContain("playerid: should be provided");
    }
    
    [Fact]
    public async Task ConfirmPlayer_PlayerNotExist_ExceptionIsReturned()
    {
        //Act
        var result = await Client.GetAsync(new Uri($"api/confirmPlayer?playerid={Guid.NewGuid()}", UriKind.Relative));
        var message =(await result.Content.ReadAsStringAsync()).ToLower();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        message.ShouldContain("validation failed");
        message.ShouldContain("playerid: does not exists");
    }
    
    [Fact]
    public async Task ConfirmPlayer_ValidPlayerId_PlayerShouldBeConfirmed()
    {
        //Arrange
        var player= await CreateAPlayerInDatabase(confirmed:false);

        //Act
        var result = await Client.GetAsync(new Uri($"api/confirmPlayer?playerid={player.Id}", UriKind.Relative));
        var confirmedPlayer = await GetCreatedPlayerFromDatabase();
        
        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        confirmedPlayer.Id.ShouldBe(player.Id);
        confirmedPlayer.Confirmed.ShouldBe(true);
    }

    [Fact]
    public async Task CheckPlayer_ValidLoginInfoAndPlayerConfirmed_Returns201()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase(confirmed: true);
        AddAuthenticationHeader(dbPlayer.Email,dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri($"api/checkPlayer", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CheckPlayer_ValidLoginInfoAndPlayerNotConfirmed_Returns401()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase(confirmed: false);
        AddAuthenticationHeader(dbPlayer.Email, dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri($"api/checkPlayer", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CheckPlayer_InvalidLoginInfo_Returns401()
    {
        //Arrange
        var dbPlayer = await CreateAPlayerInDatabase(confirmed: true);
        AddAuthenticationHeader("Wrong Email", dbPlayer.Password);

        //Act
        var result = await Client.GetAsync(new Uri($"api/checkPlayer", UriKind.Relative));

        //Assert
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}