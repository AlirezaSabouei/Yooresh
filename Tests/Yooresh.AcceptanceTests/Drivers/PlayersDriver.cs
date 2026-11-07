using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Yooresh.API;
using Yooresh.Application.Accounts.Dto;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.AcceptanceTests.Drivers;

public class PlayersDriver(WebApplicationFactory<Program> factory)
    : DriverBase<Player, PlayerDto>(factory)
{
    /// <summary>
    /// Persists the player with a hashed password while leaving the caller's instance untouched,
    /// so login tests can send the original plain-text password.
    /// </summary>
    public override async Task<Player> AddEntityToDatabaseAsync(Player player)
    {
        var passwordHasher = Factory.Services.GetRequiredService<IPasswordHasher<BaseEntity>>();

        var storedPlayer = new Player(player.Name, player.Email, player.Password)
        {
            Id = player.Id,
            Role = player.Role,
            Confirmed = player.Confirmed,
            ConfirmationCode = player.ConfirmationCode,
            RefreshTokens = player.RefreshTokens,
            CreateDate = player.CreateDate,
            UpdateDate = player.UpdateDate
        };
        storedPlayer.Password = passwordHasher.HashPassword(storedPlayer, player.Password);

        await Collection.InsertOneAsync(storedPlayer);
        return storedPlayer;
    }
}
