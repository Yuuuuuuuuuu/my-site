namespace GoPractice.Shared.Options;

public class AuthDemoOptions
{
    public const string SectionName = "AuthDemo";

    public bool Enabled { get; set; } = true;

    public List<AuthDemoUser> Users { get; set; } = new();
}

public class AuthDemoUser
{
    public string UserId { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string[] Roles { get; set; } = Array.Empty<string>();
}
