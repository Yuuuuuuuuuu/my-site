using GoPractice.Domain.Common;
using GoPractice.Shared.Results;

namespace GoPractice.Application.Interfaces;

public interface ICrudAppService<TDto, in TCreateRequest, in TUpdateRequest>
{
    Task<TDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<PagedResult<TDto>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);

    Task<TDto> CreateAsync(TCreateRequest request, CancellationToken cancellationToken = default);

    Task<TDto> UpdateAsync(long id, TUpdateRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
