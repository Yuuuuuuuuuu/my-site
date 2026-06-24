using SqlSugar;

namespace GoPractice.Domain.Common;

public abstract class BaseEntity
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.Now;

    public DateTime? UpdateTime { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public bool IsDeleted { get; set; }
}
