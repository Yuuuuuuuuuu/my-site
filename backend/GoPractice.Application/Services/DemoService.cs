using GoPractice.Application.Dtos.Demo;
using GoPractice.Application.Interfaces;
using GoPractice.Domain.Entities;
using GoPractice.Shared.Exceptions;

namespace GoPractice.Application.Services;

public class DemoService : CrudAppService<DemoRecord, DemoDto, DemoCreateRequest, DemoUpdateRequest>, IDemoService
{
    public DemoService(IDemoRepository demoRepository) : base(demoRepository)
    {
    }

    protected override Task ValidateCreateAsync(DemoCreateRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BusinessException("名称不能为空。");
        }

        return Task.CompletedTask;
    }

    protected override Task ValidateUpdateAsync(long id, DemoUpdateRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BusinessException("名称不能为空。");
        }

        return Task.CompletedTask;
    }

    protected override DemoDto MapToDto(DemoRecord entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        IsEnabled = entity.IsEnabled,
        CreateTime = entity.CreateTime
    };

    protected override DemoRecord MapCreateRequest(DemoCreateRequest request) => new()
    {
        Name = request.Name.Trim(),
        Description = request.Description?.Trim(),
        CreateBy = string.IsNullOrWhiteSpace(request.CreateBy) ? "system" : request.CreateBy.Trim(),
        CreateTime = DateTime.Now,
        IsEnabled = true
    };

    protected override void MapUpdateRequest(DemoRecord entity, DemoUpdateRequest request)
    {
        entity.Name = request.Name.Trim();
        entity.Description = request.Description?.Trim();
        entity.IsEnabled = request.IsEnabled;
    }
}
