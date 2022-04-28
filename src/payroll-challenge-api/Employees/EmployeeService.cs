using payroll_challenge_api.Db;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

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
            Id = x.EmployeeId,
            Name = x.Name
        });
    }

    public async Task<EmployeeViewModel> GetById(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) throw new NotFoundException();

        return new EmployeeViewModel
        {
            Id = employee.EmployeeId,
            Name = employee.Name
        };
    }

    public async Task<EmployeeViewModel> Add(string name)
    {
        var employee = new Employee {Name = name};
        await _context.AddAsync(employee);
        await _context.SaveChangesAsync();

        return new EmployeeViewModel
        {
            Id = employee.EmployeeId,
            Name = employee.Name
        };
    }
}