using NUnit.Framework;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Yooresh.Application.Players.Dto;

namespace Yooresh.AcceptanceTests.Controllers.Players;

public partial class PlayersControllerTests
{
    [Test]
    public async Task Login_EmptyEmail_Returns400()
    {
        var loginRequest = new LoginDto()
        {
            Email = string.Empty,
            Password = "Some Password",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(2);
        errors.All(a => a.Contains(nameof(LoginDto.Email))).ShouldBeTrue();
    }

    [Test]
    public async Task Login_InvalidEmail_Returns400()
    {
        var loginRequest = new LoginDto()
        {
            Email = "invalid email",
            Password = "Some Password",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(LoginDto.Email))).ShouldBeTrue();
    }

    [Test]
    public async Task Login_EmptyPassword_Returns400()
    {
        var loginRequest = new LoginDto()
        {
            Email = "a@gmail.com",
            Password = string.Empty,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await result.Content.ReadFromJsonAsync<List<string>>();
        errors!.Count.ShouldBe(1);
        errors.All(a => a.Contains(nameof(LoginDto.Password))).ShouldBeTrue();
    }

    [Test]
    public async Task Login_PlayerNotExist_Returns401()
    {
        var loginRequest = new LoginDto()
        {
            Email = "a@a.gmail.com",
            Password = "Some Password",
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Login_EmailNotCaseSensetive()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.RefreshTokens.Clear();
        user.Email = "test@gmail.com";
        user.Confirmed = true;
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var loginRequest = new LoginDto()
        {
            Email = user.Email.ToUpper(),
            Password = user.Password,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Login_PasswordShouldBeCaseSensetive()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.RefreshTokens.Clear();
        user.Email = "test@gmail.com";
        user.Confirmed = true;
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var loginRequest = new LoginDto()
        {
            Email = user.Email,
            Password = user.Password.ToUpper(),
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Login_PlayerExist_ReturnsTokens()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.RefreshTokens.Clear();
        user.Email = "Test@gmail.com";
        user.Confirmed = true;
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var loginRequest = new LoginDto()
        {
            Email = user.Email,
            Password = user.Password,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        Dictionary<string, string> tokens = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        tokens["token"].ShouldNotBeEmpty();
        tokens["refreshToken"].ShouldNotBeEmpty();
    }

    [Test]
    public async Task Login_PlayerExistButNotConfirmed_Returns401()
    {
        var user = _playersDriver.CreateEntityInstance();
        user.RefreshTokens.Clear();
        user.Email = "test@gmail.com";
        user.Confirmed = false;
        await _playersDriver.AddEntityToDatabaseAsync(user);
        var loginRequest = new LoginDto()
        {
            Email = user.Email,
            Password = user.Password,
        };

        var result = await _playersDriver.HttpClient.PostAsJsonAsync("api/players/login", loginRequest);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
