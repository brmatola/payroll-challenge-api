using payroll_challenge_api.Db;
using payroll_challenge_api.Dependents.Model;

namespace payroll_challenge_api.Dependents;

public class DependentService
{
    private readonly EmployeeContext _context;

    public DependentService(EmployeeContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DependentViewModel>> GetDependents(Guid employeeId)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee == null) throw new NotFoundException();

        return employee.Dependents.Select(x => new DependentViewModel
        {
            Id = x.DependentId,
            Name = x.Name
        });
    }
}