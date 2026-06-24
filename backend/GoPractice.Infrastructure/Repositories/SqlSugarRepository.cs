using GoPractice.Application.Interfaces;
using GoPractice.Domain.Common;
using GoPractice.Shared.Results;
using SqlSugar;

namespace GoPractice.Infrastructure.Repositories;

public class SqlSugarRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly ISqlSugarClient Db;
    protected readonly ICurrentUser CurrentUser;

    public SqlSugarRepository(ISqlSugarClient db, ICurrentUser currentUser)
    {
        Db = db;
        CurrentUser = currentUser;
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await Db.Queryable<TEntity>()
            .Where(x => !x.IsDeleted && x.Id == id)
            .FirstAsync();
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        RefAsync<int> totalCount = 0;

        var items = await Db.Queryable<TEntity>()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.Id)
            .ToPageListAsync(request.SafePageIndex, request.SafePageSize, totalCount);

        return new PagedResult<TEntity>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;
        entity.CreateBy = ResolveUserName();
        entity.UpdateBy = ResolveUserName();

        await Db.Insertable(entity).ExecuteCommandAsync();
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdateTime = DateTime.Now;
        entity.UpdateBy = ResolveUserName();

        return await Db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> SoftDeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.IsDeleted = true;
        entity.UpdateTime = DateTime.Now;
        entity.UpdateBy = ResolveUserName();
        return await Db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    protected virtual string ResolveUserName()
    {
        return CurrentUser.IsAuthenticated ? CurrentUser.UserName : "system";
    }
}
