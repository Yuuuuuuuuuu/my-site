using GoPractice.Domain.Common;
using GoPractice.Shared.Results;

namespace GoPractice.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<PagedResult<TEntity>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);

    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteAsync(long id, CancellationToken cancellationToken = default);
}
