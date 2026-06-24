using GoPractice.Application.Dtos.Demo;
using GoPractice.Application.Interfaces;
using GoPractice.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoPractice.Api.Controllers;

[Route("api/[controller]")]
public class DemoController : BaseApiController
{
    private readonly IDemoService _demoService;

    public DemoController(IDemoService demoService)
    {
        _demoService = demoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedAsync([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await _demoService.GetPagedAsync(request, cancellationToken);
        return Success(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var result = await _demoService.GetByIdAsync(id, cancellationToken);
        return Success(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DemoCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _demoService.CreateAsync(request, cancellationToken);
        return Success(result, "创建成功。");
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] DemoUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _demoService.UpdateAsync(id, request, cancellationToken);
        return Success(result, "更新成功。");
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _demoService.DeleteAsync(id, cancellationToken);
        return Success("删除成功。");
    }

    [Authorize]
    [HttpGet("secure-ping")]
    public IActionResult SecurePing()
    {
        return Success(new
        {
            Message = "secure endpoint is working",
            ServerTime = DateTime.UtcNow
        });
    }
}
