using GoPractice.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace GoPractice.Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult Success(string message = "ok")
    {
        return Ok(ApiResult.Ok(message));
    }

    protected IActionResult Success<T>(T? data, string message = "ok")
    {
        return Ok(ApiResult<T>.Ok(data, message));
    }
}
