using payroll_challenge_api.Db;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits.Employees;

public class RegularEmployeeBenefitProvider : EmployeeBenefitProvider
{
    protected override Task<DollarsPerYear> GetEmployeeCost()
    {
        return Task.FromResult(new DollarsPerYear(1000));
    }

    public RegularEmployeeBenefitProvider(Employee employee, IDependentBenefitProviderFactory dependentBenefitProviderFactory) : base(employee, dependentBenefitProviderFactory)
    {
    }
}