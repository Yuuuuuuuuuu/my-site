using Microsoft.AspNetCore.Mvc;

namespace GoPractice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] string? name)
    {
        name ??= "World";
        return Ok(new { message = $"Hello, {name}!" });
    }
}
