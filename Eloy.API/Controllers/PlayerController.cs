using Eloy.Application.Players.Commands;
using Eloy.Application.Players.Queries;
using Eloy.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eloy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayersController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<Player>> GetPlayer()
    {
        var query = new GetPlayerQuery()
        {
            Email = HttpContext.User.Identity.Name
        };
        return await Mediator.Send(query);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> CreatePlayer([FromBody] CreatePlayerCommand command)
    {
        try
        {
            return await Mediator.Send(command);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
