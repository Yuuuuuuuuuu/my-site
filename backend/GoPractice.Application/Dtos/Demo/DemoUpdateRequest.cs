namespace GoPractice.Application.Dtos.Demo;

public class DemoUpdateRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; } = true;
}
