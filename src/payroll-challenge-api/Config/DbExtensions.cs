using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using payroll_challenge_api.Db;

namespace payroll_challenge_api.Config;

internal static class DbExtensions
{
    public static void UseEmployeeContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<EmployeeContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("EmployeeContext");

            options.UseNpgsql(connectionString);
        });
    }

    public static void CreateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
        
        context.Database.EnsureCreated();
        SeedData.Initialize(context);
    }
}