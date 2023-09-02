using Eloy.Application.ResourceBuildings.Commands;
using Eloy.Domain.Entities.ResourceBuildings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eloy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ResourceBuildingController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<List<ResourceBuilding>>> SeedBasicDataForResourceBuildings([FromBody] SeedResourceBuildingsCommand command)
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