namespace payroll_challenge_api.Db;

public interface IDependentRepository
{
    Task<Dependent> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id);
}

internal class DependentRepository : IDependentRepository
{
    private readonly EmployeeContext _context;

    public DependentRepository(EmployeeContext context)
    {
        _context = context;
    }

    public async Task<Dependent> GetByIdAsync(Guid id)
    {
        var dependent = await _context.Dependents.FindAsync(id);
        if (dependent == null) throw new NotFoundException();

        return dependent;
    }

    public async Task RemoveAsync(Guid id)
    {
        var dependent = await _context.Dependents.FindAsync(id);
        if (dependent == null) throw new NotFoundException();

        _context.Dependents.Remove(dependent);
        await _context.SaveChangesAsync();
    }
}