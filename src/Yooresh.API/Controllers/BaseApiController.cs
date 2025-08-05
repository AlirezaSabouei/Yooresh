using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Yooresh.API.Controllers;

public class BaseApiController(IMapper mapper) : ControllerBase
{
    protected Guid PlayerId=>Guid.Parse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
    
    protected readonly IMapper _mapper = mapper;
    protected ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}