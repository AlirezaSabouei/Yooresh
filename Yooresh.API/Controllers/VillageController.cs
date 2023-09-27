using Yooresh.Application.Villages.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Application.Villages.Queries;

namespace Yooresh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
    public class VillageController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<Village>> GetVillage()
        {
            try
            {
                var playerId = new Guid(HttpContext.User.FindFirst(nameof(Player.Id))!.Value);
                var query = new GetVillageQuery()
                {
                    PlayerId = playerId
                };
                return await Mediator.Send(query);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }
  
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Village>> GetVillage([FromBody]CreateVillageCommand command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }
  
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
