using payroll_challenge_api.Db;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

public class EmployeeManagerService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeManagerService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetEmployees()
    {
        var employees = await _repository.GetEmployees();
        return employees.Select(x => new EmployeeViewModel
        {
            Id = x.EmployeeId,
            Name = x.Name
        });
    }

    public async Task<EmployeeViewModel> GetById(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);

        return new EmployeeViewModel
        {
            Id = employee.EmployeeId,
            Name = employee.Name
        };
    }

    public async Task<EmployeeViewModel> Add(string name)
    {
        var employee = await _repository.AddAsync(name);

        return new EmployeeViewModel
        {
            Id = employee.EmployeeId,
            Name = employee.Name
        };
    }

    public async Task DeleteById(Guid id)
    {
        await _repository.RemoveAsync(id);
    }
}