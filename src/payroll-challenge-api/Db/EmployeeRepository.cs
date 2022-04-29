using Microsoft.EntityFrameworkCore;

namespace payroll_challenge_api.Db;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee> GetByIdAsync(Guid id);
    Task<Employee> AddAsync(string name);
    Task RemoveAsync(Guid id);
    public Task SaveAsync();
}

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeContext _context;

    public EmployeeRepository(EmployeeContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(Guid id)
    {
        var employee = _context.Employees
            .Include(x => x.Dependents)
            .SingleOrDefault(x => x.EmployeeId == id);
        if (employee == null) throw new NotFoundException();

        return employee;
    }

    public async Task<Employee> AddAsync(string name)
    {
        var employee = new Employee {Name = name};
        await _context.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task RemoveAsync(Guid id)
    {
        var employee = await GetByIdAsync(id);

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}