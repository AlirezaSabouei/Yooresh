using Microsoft.AspNetCore.Mvc.Testing;
using Yooresh.API;
using Yooresh.Application.Accounts.Dto;
using Yooresh.Domain.Entities.Accounts;

namespace Yooresh.AcceptanceTests.Drivers;

public class PlayersDriver(WebApplicationFactory<Program> factory)
    : DriverBase<Player, PlayerDto>(factory)
{
}