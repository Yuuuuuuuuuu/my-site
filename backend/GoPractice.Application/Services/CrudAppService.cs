using GoPractice.Application.Interfaces;
using GoPractice.Domain.Common;
using GoPractice.Shared.Exceptions;
using GoPractice.Shared.Results;

namespace GoPractice.Application.Services;

public abstract class CrudAppService<TEntity, TDto, TCreateRequest, TUpdateRequest>
    : ICrudAppService<TDto, TCreateRequest, TUpdateRequest>
    where TEntity : BaseEntity, new()
{
    private readonly IRepository<TEntity> _repository;

    protected CrudAppService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            throw new BusinessException("数据不存在。", 4040);
        }

        return MapToDto(entity);
    }

    public virtual async Task<PagedResult<TDto>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetPagedAsync(request, cancellationToken);

        return new PagedResult<TDto>
        {
            TotalCount = result.TotalCount,
            Items = result.Items.Select(MapToDto).ToArray()
        };
    }

    public virtual async Task<TDto> CreateAsync(TCreateRequest request, CancellationToken cancellationToken = default)
    {
        await ValidateCreateAsync(request, cancellationToken);

        var entity = MapCreateRequest(request);
        var saved = await _repository.InsertAsync(entity, cancellationToken);
        return MapToDto(saved);
    }

    public virtual async Task<TDto> UpdateAsync(long id, TUpdateRequest request, CancellationToken cancellationToken = default)
    {
        await ValidateUpdateAsync(id, request, cancellationToken);

        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            throw new BusinessException("数据不存在。", 4040);
        }

        MapUpdateRequest(entity, request);
        var updated = await _repository.UpdateAsync(entity, cancellationToken);
        if (!updated)
        {
            throw new BusinessException("更新失败。");
        }

        return MapToDto(entity);
    }

    public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var deleted = await _repository.SoftDeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            throw new BusinessException("数据不存在或删除失败。", 4040);
        }
    }

    protected virtual Task ValidateCreateAsync(TCreateRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected virtual Task ValidateUpdateAsync(long id, TUpdateRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected abstract TDto MapToDto(TEntity entity);

    protected abstract TEntity MapCreateRequest(TCreateRequest request);

    protected abstract void MapUpdateRequest(TEntity entity, TUpdateRequest request);
}
