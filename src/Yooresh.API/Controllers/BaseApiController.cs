﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yooresh.Domain.Entities.Players;

namespace Yooresh.API.Controllers;

public class BaseApiController(IMapper mapper) : ControllerBase
{
    protected Guid PlayerId=>new(HttpContext.User.FindFirst(nameof(Player.Id))!.Value);
    
    protected readonly IMapper _mapper = mapper;
    protected ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}