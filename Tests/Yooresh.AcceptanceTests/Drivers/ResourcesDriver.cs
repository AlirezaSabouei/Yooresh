using Microsoft.AspNetCore.Mvc.Testing;
using Yooresh.API;
using Yooresh.Application.Resources.Dto;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.AcceptanceTests.Drivers;

public class ResourcesDriver(WebApplicationFactory<Program> factory) 
    : DriverBase<Resource, ResourceDto>(factory)
{
}
