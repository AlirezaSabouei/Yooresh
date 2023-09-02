using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eloy.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}