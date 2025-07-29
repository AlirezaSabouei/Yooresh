using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yooresh.Application.Players.Commands;
using Yooresh.Application.Players.Dto;
using Yooresh.Application.Players.Queries;

namespace Yooresh.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController(IMapper mapper) : BaseApiController(mapper)
{
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

    [HttpPost("signup")]
    public async Task<ActionResult<PlayerDto>> SignUp([FromBody] CreatePlayerCommand command)
    {
        // command.SiteAddress = $"{Request.Scheme}://{Request.Host.Value}/api/confirmplayer?playerid=";
        var result = await Mediator.Send(command);
        return _mapper.Map<PlayerDto>(result);
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<bool>> ConfirmPlayer([FromBody] string confirmationCode)
    {
        var command = new ConfirmPlayerCommand()
        {
            PlayerId = PlayerId,
            ConfirmationCode = confirmationCode
        };
        return await Mediator.Send(command);
    }


    [HttpPost("login")]
    public async Task<ActionResult<Dictionary<string, string>?>> Login([FromBody] LoginDto request)
    {
        LoginCommand command = new()
        {
            Email = request.Email,
            Password = request.Password
        };
        Dictionary<string, string>? result = await Mediator.Send(command);
        if (result == null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<Dictionary<string, string>?>> Refresh([FromBody] RefreshRequest request)
    {
        RefreshCommand command = new()
        {
            RefreshToken = request.RefreshToken
        };
        Dictionary<string, string>? result = await Mediator.Send(command);
        if (result == null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }
}