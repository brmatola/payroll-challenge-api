using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<DependentViewModel> GetById(Guid id)
    {
        var dependent = await _context.Dependents.FindAsync(id);
        if (dependent == null) throw new NotFoundException();

        return new DependentViewModel
        {
            Id = dependent.DependentId,
            Name = dependent.Name
        };
    }

    public async Task<IEnumerable<DependentViewModel>> GetDependents(Guid employeeId)
    {
        var employee = _context.Employees
            .Include(x => x.Dependents)
            .SingleOrDefault(x => x.EmployeeId == employeeId);
        if (employee == null) throw new NotFoundException();

        return employee.Dependents.Select(x => new DependentViewModel
        {
            Id = x.DependentId,
            Name = x.Name
        });
    }

    public async Task<DependentViewModel> AddDependent(Guid employeeId, string name)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee == null) throw new NotFoundException();

        var dependent = new Dependent {Name = name};
        employee.Dependents.Add(dependent);
        await _context.SaveChangesAsync();

        return new DependentViewModel
        {
            Id = dependent.DependentId,
            Name = dependent.Name
        };
    }

    public async Task DeleteById(Guid dependentId)
    {
        var dependent = await _context.Dependents.FindAsync(dependentId);
        if (dependent == null) throw new NotFoundException();

        _context.Dependents.Remove(dependent);
        await _context.SaveChangesAsync();
    }
}