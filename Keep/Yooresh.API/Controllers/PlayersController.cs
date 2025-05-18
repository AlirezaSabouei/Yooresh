using Yooresh.Application.Players.Commands;
using Yooresh.Application.Players.Queries;
using Yooresh.Domain.Entities.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Yooresh.Application.Players.Dto;

namespace Yooresh.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
public class PlayersController : BaseApiController
{
    public PlayersController(IMapper mapper) : base(mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PlayerDto>> GetPlayer()
    {
        var emil = HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
        var query = new GetPlayerQuery()
        {
            Email = emil
        };
        var result = await Mediator.Send(query);
        var playerDto = _mapper.Map<PlayerDto>(result);
        return playerDto;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> SignUp([FromBody] CreatePlayerCommand command)
    {
        try
        {
            command.SiteAddress = $"{Request.Scheme}://{Request.Host.Value}/api/confirmplayer?playerid=";
            return await Mediator.Send(command);
        }
        catch (FluentValidation.ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/ConfirmPlayer")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> ConfirmPlayer([FromQuery] Guid playerId)
    {
        try
        {
            var command = new ConfirmPlayerCommand()
            {
                PlayerId = playerId
            };
            return await Mediator.Send(command);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/CheckPlayer")]
    public ActionResult CheckPlayer()
    {
        return NoContent();
    }
}