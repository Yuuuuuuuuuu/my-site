using GoPractice.Application.Interfaces;
using GoPractice.Infrastructure.Persistence;
using GoPractice.Infrastructure.Repositories;
using GoPractice.Infrastructure.Services;
using GoPractice.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace GoPractice.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.SectionName));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<AuthDemoOptions>(configuration.GetSection(AuthDemoOptions.SectionName));

        var dbOptions = configuration.GetSection(DbOptions.SectionName).Get<DbOptions>() ?? new DbOptions();
        if (string.IsNullOrWhiteSpace(dbOptions.ConnectionString))
        {
            throw new InvalidOperationException("Database:ConnectionString is not configured.");
        }

        var client = SqlSugarSetup.Create(dbOptions);
        services.AddSingleton<ISqlSugarClient>(client);
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, DemoAuthService>();
        services.AddScoped<IDemoRepository, DemoRepository>();

        return services;
    }
}
