using payroll_challenge_api.Db;
using payroll_challenge_api.Dependents.Model;

namespace payroll_challenge_api.Dependents;

public class DependentService
{
    private readonly IDependentRepository _dependentRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public DependentService(IDependentRepository dependentRepository, IEmployeeRepository employeeRepository)
    {
        _dependentRepository = dependentRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<DependentViewModel> GetById(Guid id)
    {
        var dependent = await _dependentRepository.GetByIdAsync(id);

        return new DependentViewModel
        {
            Id = dependent.DependentId,
            Name = dependent.Name
        };
    }

    public async Task<IEnumerable<DependentViewModel>> GetDependents(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        return employee.Dependents.Select(x => new DependentViewModel
        {
            Id = x.DependentId,
            Name = x.Name
        });
    }

    public async Task<DependentViewModel> AddDependent(Guid employeeId, string name)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        var dependent = new Dependent {Name = name};
        employee.Dependents.Add(dependent);
        await _employeeRepository.SaveAsync();

        return new DependentViewModel
        {
            Id = dependent.DependentId,
            Name = dependent.Name
        };
    }

    public async Task DeleteById(Guid dependentId)
    {
        await _dependentRepository.RemoveAsync(dependentId);
        
    }
}