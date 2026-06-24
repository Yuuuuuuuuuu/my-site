using Microsoft.AspNetCore.Mvc;

namespace GoPractice.Api.Controllers;

[Route("api/[controller]")]
public class HealthController : BaseApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Success("service is running");
    }
}
