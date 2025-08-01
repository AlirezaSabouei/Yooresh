using Microsoft.AspNetCore.Mvc.Testing;
using Yooresh.API;
using Yooresh.Application.Players.Dto;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.AcceptanceTests.Drivers;

public class PlayersDriver(WebApplicationFactory<Program> factory)
    : DriverBase<Player, PlayerDto>(factory)
{
}