using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Domain.Players;

namespace Yooresh.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected Guid PlayerId=>new Guid(HttpContext.User.FindFirst(nameof(Player.Id))!.Value);
    
    protected readonly IMapper _mapper;

    public BaseApiController(IMapper mapper)
    {
        _mapper = mapper;
    }
    protected ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}