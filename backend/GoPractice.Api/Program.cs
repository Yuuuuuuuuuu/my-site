using Microsoft.EntityFrameworkCore;
using GoPractice.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Database
var connectionString = builder.Configuration.GetConnectionString("Default");
if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );
}

// CORS — allow frontend dev server
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Apply migrations on startup (skip if no DB configured)
var dbConfigured = !string.IsNullOrEmpty(connectionString);
if (dbConfigured)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try { db.Database.Migrate(); }
        catch (Exception ex)
        {
            Console.WriteLine($"DB migration skipped: {ex.Message}");
        }
    }
}

app.UseCors();
app.MapControllers();

app.Run();
