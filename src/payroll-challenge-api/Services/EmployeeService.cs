using System.Collections.Generic;
using System.Linq;
using payroll_challenge_api.Db;

namespace payroll_challenge_api.Services;

public class EmployeeService
{
    private readonly EmployeeContext _context;

    public EmployeeService(EmployeeContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeViewModel> GetEmployees()
    {
        return _context.Employees.Select(x => new EmployeeViewModel
        {
            Id = x.EmployeeId.ToString(),
            Name = x.Name
        });
    }
}