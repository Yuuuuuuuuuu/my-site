using GoPractice.Domain.Common;
using SqlSugar;

namespace GoPractice.Domain.Entities;

[SugarTable("demo_records")]
public class DemoRecord : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; } = true;
}
