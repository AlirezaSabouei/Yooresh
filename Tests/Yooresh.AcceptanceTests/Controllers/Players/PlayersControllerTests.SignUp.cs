using NUnit.Framework;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Yooresh.Application.Players.Commands;
using Yooresh.Application.Players.Dto;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.AcceptanceTests.Controllers.Players;

public partial class PlayersControllerTests
{
    [Test]
    public async Task SignUp_InvalidName_Returns400()
    {
        var command = new CreatePlayerCommand()
        {
            Name = string.Empty,
            Email = "a@a.com",
            Password = "Some Password@123",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(CreatePlayerCommand.Name))).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_InvalidEmail_Returns400()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = string.Empty,
            Password = "Some Password@123",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(3);
        errors.All(a => a.Contains(nameof(LoginDto.Email))).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_NotUniqueEmail_Returns400()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.Email = "test@gmail.com";
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = user.Email,
            Password = "Some Password@123",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(LoginDto.Email))).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_NotUniqueEmailWithCaseDifference_Returns400()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.Email = "test@gmail.com";
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = user.Email.ToUpper(),
            Password = "Some Password@123",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(LoginDto.Email))).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_EmptyPassword_Returns400()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "a@gmail.com",
            Password = string.Empty,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(2);
        errors.All(a => a.Contains(nameof(LoginDto.Password))).ShouldBeTrue();
    }

    [Test]
    [TestCase("12345")]
    [TestCase("aksdj")]
    public async Task SignUp_WeakPassword_Returns400(string password)
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "a@gmail.com",
            Password = password,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(LoginDto.Password))).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_ValidInput_PlayerCreated()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "A@GMAIL.COM",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        await AssertDatabaseCreatedPlayer(playerDto!);
    }

    private static async Task<PlayerDto?> AssertSuccessResult(HttpResponseMessage result)
    {
        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.Created);
        PlayerDto? player = await result.Content.ReadFromJsonAsync<PlayerDto>();
        player.ShouldNotBeNull();
        return player;
    }

    private async Task<Player?> AssertDatabaseCreatedPlayer(PlayerDto player)
    {
        var createdPlayer = await _playersDriver.GetEntityFromDatabaseAsync(player!.Id);
        createdPlayer.ShouldNotBeNull();
        return createdPlayer;
    }

    [Test]
    public async Task SignUp_PlayerCreated_ConfirmedShouldBeFalse()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "a@gmail.com",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        playerDto!.Confirmed.ShouldBeFalse();
        var createdPlayer = await AssertDatabaseCreatedPlayer(playerDto!);
        createdPlayer!.Confirmed.ShouldBeFalse();
    }

    [Test]
    public async Task SignUp_PlayerCreated_ConfirmedCodeShouldCreatedInDatabase()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "a@gmail.com",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        playerDto!.Confirmed.ShouldBeFalse();
        var createdPlayer = await AssertDatabaseCreatedPlayer(playerDto!);
        Guid.TryParse(createdPlayer!.ConfirmationCode, out _).ShouldBeTrue();
    }

    [Test]
    public async Task SignUp_PlayerCreated_RoleShouldBeSimplePlayer()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "a@gmail.com",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        playerDto!.Role.ShouldBe(RoleDto.SimplePlayer);
        var createdPlayer = await AssertDatabaseCreatedPlayer(playerDto!);
        createdPlayer!.Role.ShouldBe(Role.SimplePlayer);
    }

    [Test]
    public async Task SignUp_PlayerCreated_EmailShouldBeStoredInLowerCase()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "A@GMAIL.COM",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        playerDto!.Email.ShouldBe(command.Email.ToLower());
        var createdPlayer = await AssertDatabaseCreatedPlayer(playerDto!);
        createdPlayer!.Email.ShouldBe(command.Email.ToLower());
    }

    [Test]
    public async Task SignUp_PlayerCreated_PasswordShouldBeHashed()
    {
        var command = new CreatePlayerCommand()
        {
            Name = "some name",
            Email = "A@GMAIL.COM",
            Password = "some password123@",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/signup", command);

        PlayerDto? playerDto = await AssertSuccessResult(result);
        var createdPlayer = await AssertDatabaseCreatedPlayer(playerDto!);
        createdPlayer!.Password.ShouldNotBe(command.Password);
    }
}
