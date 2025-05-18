using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Villages.Application.Villages.Commands;
using Villages.Application.Villages.Dto;
using Villages.Application.Villages.Queries;
using Villages.Domain.Common.Exceptions;

namespace Villages.API.Controllers;

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
    public async Task<ActionResult<VillageDto>> GetVillage()
    {
        try
        {
            var query = new GetVillageQuery()
            {
                PlayerId = PlayerId
            };
            var result=await Mediator.Send(query);
            return _mapper.Map<VillageDto>(result);
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
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}