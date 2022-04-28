using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using payroll_challenge_api.Db;

namespace payroll_challenge_api.Config;

internal static class DbExtensions
{
    public static void UseEmployeeContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<EmployeeContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("EmployeeContext");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseNpgsql(connectionString);
            }
            else
            {
                Console.Out.WriteLine("No database configured");
            }
        });
    }

    public static void CreateDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployeeContext>();

        try
        {
            context.Database.EnsureCreated();
            SeedData.Initialize(context);
        }
        catch (Exception e)
        {
            // GULP - workaround TODO: improve db handling
        }
    }
}