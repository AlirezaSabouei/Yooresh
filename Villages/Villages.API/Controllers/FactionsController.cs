using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Villages.Application.Factions.Dto;
using Villages.Application.Factions.Queries;

namespace Villages.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
public class FactionsController : BaseApiController
{
    public FactionsController(IMapper mapper) : base(mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<FactionDto>>> GetFactions()
    {
        try
        {
            var query = new GetFactionsQuery();
            
            var result=await Mediator.Send(query);

            return _mapper.Map<List<FactionDto>>(result);
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