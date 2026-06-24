using GoPractice.Application.Interfaces;
using GoPractice.Application.Services;

namespace GoPractice.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiSkeleton(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "GoPractice API", Version = "v1" });
            options.AddJwtSwagger();
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddJwtAuth(configuration);
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDemoService, DemoService>();

        return services;
    }
}
