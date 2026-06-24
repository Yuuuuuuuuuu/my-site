namespace GoPractice.Shared.Options;

public class DbOptions
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = string.Empty;

    public string DbType { get; set; } = "MySql";

    public bool AutoInitSchema { get; set; } = true;
}
