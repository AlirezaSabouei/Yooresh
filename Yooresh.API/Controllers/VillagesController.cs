using AutoMapper;
using Yooresh.Application.Villages.Commands;
using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Application.ResourceBuildings.Dto;
using Yooresh.Application.Villages.Queries;
using Yooresh.Domain.Common;
using Yooresh.Domain.Entities.Buildings;

namespace Yooresh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
    public class VillagesController : BaseApiController
    {
        public VillagesController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> VillageExists()
        {
            try
            {
                var query = new VillageExistsQuery()
                {
                    PlayerId = PlayerId
                };
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

        [HttpGet]
        public async Task<ActionResult<Village>> GetVillage()
        {
            try
            {
                var query = new GetVillageQuery()
                {
                    PlayerId = PlayerId
                };
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

        [HttpPost]
        public async Task<ActionResult<bool>> CreateVillage([FromBody] CreateVillageCommandDto createVillageCommandDto)
        {
            try
            {
                var command = _mapper.Map<CreateVillageCommand>(createVillageCommandDto);
                command.PlayerId = PlayerId;

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

        [HttpGet("AvailableResourceBuildingUpgrades")]
        public async Task<ActionResult<List<ResourceBuildingDto>>> GetAvailableResourceBuildingUpgrades()
        {
            var query = new GetAvailableResourceBuildingUpgradesQuery()
            {
                PlayerId = PlayerId
            };
            var result= await Mediator.Send(query);
            return _mapper.Map<List<ResourceBuildingDto>>(result);
        }

        [HttpPost("UpgradeResourceBuilding")]
        public async Task<ActionResult<bool>> UpgradeResourceBuilding([FromBody] UpgradeResourceBuildingCommandDto startBuildingUpgradeCommandDto)
        {
            try
            {
                var command = _mapper.Map<UpdateResourceBuildingCommand>(startBuildingUpgradeCommandDto);
                command.PlayerId = PlayerId;
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
    }
}