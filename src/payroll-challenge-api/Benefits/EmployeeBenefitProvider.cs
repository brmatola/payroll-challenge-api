using payroll_challenge_api.Db;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits;

public abstract class EmployeeBenefitProvider
{
    private readonly Employee _employee;
    private readonly IDependentBenefitProviderFactory _dependentBenefitProviderFactory;

    protected EmployeeBenefitProvider(Employee employee, IDependentBenefitProviderFactory dependentBenefitProviderFactory)
    {
        _employee = employee;
        _dependentBenefitProviderFactory = dependentBenefitProviderFactory;
    }

    public async Task<DollarsPerYear> GetBenefitCost()
    {
        var costs = await Task.WhenAll(GetEmployeeCost(), GetDependentCost());
        return costs[0] + costs[1];
    }

    protected abstract Task<DollarsPerYear> GetEmployeeCost();

    private async Task<DollarsPerYear> GetDependentCost()
    {
        var amounts = await Task.WhenAll(_employee.Dependents.Select(ResolveDependentCost));
        return amounts.Aggregate(new DollarsPerYear(0), (curr, prev) => curr + prev);
    }

    private async Task<DollarsPerYear> ResolveDependentCost(Dependent dependent)
    {
        var provider = await _dependentBenefitProviderFactory.GetProvider(dependent);
        return await provider.GetBenefitCost();
    }
}