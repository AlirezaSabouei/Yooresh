using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Yooresh.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}