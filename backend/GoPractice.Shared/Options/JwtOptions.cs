namespace GoPractice.Shared.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public bool Enabled { get; set; }

    public string Issuer { get; set; } = "GoPractice";

    public string Audience { get; set; } = "GoPractice.Client";

    public string SecretKey { get; set; } = "ReplaceWithAStrongSecretKeyAtLeast32Chars";

    public int ExpireMinutes { get; set; } = 120;
}
