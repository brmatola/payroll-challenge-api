using payroll_challenge_api.Db;

namespace payroll_challenge_api.Benefits;

public class EmployeeBenefitProviderFactory : IEmployeeBenefitProviderFactory
{
    public Task<IEmployeeBenefitProvider> GetProvider(Employee employee)
    {
        if (!string.IsNullOrEmpty(employee.Name) && employee.Name.StartsWith('A'))
        {
            return Task.FromResult((IEmployeeBenefitProvider) new DiscountEmployeeBenefitProvider());
        }

        return Task.FromResult((IEmployeeBenefitProvider) new RegularEmployeeBenefitProvider());
    }
}