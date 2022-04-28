using payroll_challenge_api.Db;

namespace payroll_challenge_api.Config;

public static class SeedData
{
    public static void Initialize(EmployeeContext context)
    {
        if (context.Employees.Any())
            return;

        var employees = new Employee[]
        {
            new Employee {Name = "Brad"},
            new Employee {Name = "Craig"},
            new Employee {Name = "Andy"},
            new Employee {Name = "Mackenzie"},
            new Employee {Name = "Peter"}
        };
        
        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}