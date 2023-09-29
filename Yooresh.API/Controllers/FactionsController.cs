using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Application.Factions.Queries;
using Yooresh.Domain.Entities.Factions;

namespace Yooresh.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
public class FactionsController : BaseApiController
{
    public FactionsController(IMapper mapper) : base(mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<Faction>>> GetFactions()
    {
        try
        {
            var query = new GetFactionsQuery();
                
            return await Mediator.Send(query);
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
}