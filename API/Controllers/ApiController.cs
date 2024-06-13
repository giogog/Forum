using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ApiController(IServiceManager serviceManager) => _serviceManager = serviceManager;
}
