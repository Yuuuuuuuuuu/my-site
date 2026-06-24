using GoPractice.Application.Interfaces;
using GoPractice.Domain.Common;
using GoPractice.Domain.Entities;
using GoPractice.Shared.Results;
using SqlSugar;

namespace GoPractice.Infrastructure.Repositories;

public class DemoRepository : SqlSugarRepository<DemoRecord>, IDemoRepository
{
    public DemoRepository(ISqlSugarClient db, ICurrentUser currentUser) : base(db, currentUser)
    {
    }

    public override async Task<PagedResult<DemoRecord>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        RefAsync<int> totalCount = 0;

        var query = Db.Queryable<DemoRecord>()
            .Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword.Trim();
            query = query.Where(x => x.Name.Contains(keyword) || (x.Description != null && x.Description.Contains(keyword)));
        }

        var items = await query
            .OrderByDescending(x => x.Id)
            .ToPageListAsync(request.SafePageIndex, request.SafePageSize, totalCount);

        return new PagedResult<DemoRecord>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
