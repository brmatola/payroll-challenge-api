using System;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using payroll_challenge_api.Config;
using payroll_challenge_api.Db;

namespace payroll_challenge_api.integrationtest;

internal class MockWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection;

    public MockWebApplicationFactory()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<EmployeeContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            else
            {
                throw new Exception("No Employee context registered");
            }

            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            var provider = services.BuildServiceProvider();
            provider.CreateDatabase();
        });
    }
}