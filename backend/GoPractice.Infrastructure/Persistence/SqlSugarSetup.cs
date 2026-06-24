using GoPractice.Domain.Entities;
using GoPractice.Shared.Options;
using SqlSugar;

namespace GoPractice.Infrastructure.Persistence;

public static class SqlSugarSetup
{
    public static SqlSugarScope Create(DbOptions options)
    {
        var config = new ConnectionConfig
        {
            ConnectionString = options.ConnectionString,
            DbType = ParseDbType(options.DbType),
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        };

        var client = new SqlSugarScope(config, db =>
        {
            db.Aop.OnLogExecuting = (sql, parameters) =>
            {
                Console.WriteLine($"[SqlSugar] {sql}");
                if (parameters?.Length > 0)
                {
                    Console.WriteLine($"[SqlSugar] params: {string.Join(", ", parameters.Select(x => $"{x.ParameterName}={x.Value}"))}");
                }
            };
        });

        if (options.AutoInitSchema)
        {
            client.DbMaintenance.CreateDatabase();
            client.CodeFirst.InitTables<DemoRecord>();
        }

        return client;
    }

    private static DbType ParseDbType(string dbType) =>
        dbType.ToLowerInvariant() switch
        {
            "mysql" => DbType.MySql,
            "sqlserver" => DbType.SqlServer,
            _ => throw new InvalidOperationException($"Unsupported database type: {dbType}")
        };
}
