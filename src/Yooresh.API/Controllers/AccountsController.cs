using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Application.Accounts.Commands;
using Yooresh.Application.Accounts.Dto;

namespace Yooresh.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IMapper mapper) : BaseApiController(mapper)
{
    [HttpPost("signup")]
    public async Task<ActionResult<PlayerDto>> SignUp([FromBody] CreatePlayerCommand command)
    {
        var result = await Mediator.Send(command);
        var dto = _mapper.Map<PlayerDto>(result);
        return Created(string.Empty, dto);
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<bool>> ConfirmPlayer([FromBody] ConfirmAccountCommand confirmAccountCommand)
    {
        return await Mediator.Send(confirmAccountCommand);
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