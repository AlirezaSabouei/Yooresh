using Microsoft.AspNetCore.Mvc.Testing;
using Yooresh.API;
using Yooresh.Application.Account.Dto;
using Yooresh.Domain.Entities.Account;

namespace Yooresh.AcceptanceTests.Drivers;

public class PlayersDriver(WebApplicationFactory<Program> factory)
    : DriverBase<Player, PlayerDto>(factory)
{
}