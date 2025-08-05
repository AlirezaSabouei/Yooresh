using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Application.Resources.Dto;
using Yooresh.Application.Resources.Queries;

namespace Yooresh.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ResourcesController(IMapper mapper) : BaseApiController(mapper)
{
    [HttpGet]
    public async Task<ActionResult<List<ResourceDto>>> GetResources()
    {
        var query = new GetResourcesQuery()
        {
            PlayerId = PlayerId
        };
        var result = await Mediator.Send(query);
        return _mapper.Map<List<ResourceDto>>(result);
    }
}
