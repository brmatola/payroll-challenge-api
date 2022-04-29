using payroll_challenge_api.Db;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits.Employees;

public class DiscountEmployeeBenefitProvider : EmployeeBenefitProvider
{
    protected override Task<DollarsPerYear> GetEmployeeCost()
    {
        return Task.FromResult(new DollarsPerYear(900));
    }

    public DiscountEmployeeBenefitProvider(Employee employee, IDependentBenefitProviderFactory dependentBenefitProviderFactory) : base(employee, dependentBenefitProviderFactory)
    {
    }
}