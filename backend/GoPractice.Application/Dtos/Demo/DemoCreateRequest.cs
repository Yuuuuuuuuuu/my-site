namespace GoPractice.Application.Dtos.Demo;

public class DemoCreateRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }
}
