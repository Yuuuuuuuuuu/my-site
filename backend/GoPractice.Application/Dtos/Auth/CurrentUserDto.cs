namespace GoPractice.Application.Dtos.Auth;

public class CurrentUserDto
{
    public string UserId { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public bool IsAuthenticated { get; set; }

    public string[] Roles { get; set; } = Array.Empty<string>();
}
