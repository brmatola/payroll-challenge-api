using Microsoft.EntityFrameworkCore;

namespace payroll_challenge_api.Db;

public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
}