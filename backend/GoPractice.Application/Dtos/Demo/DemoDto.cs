namespace GoPractice.Application.Dtos.Demo;

public class DemoDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; }

    public DateTime CreateTime { get; set; }
}
