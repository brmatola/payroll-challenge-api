namespace payroll_challenge_api.Benefits.Employees;

public class EmployeeBenefitProviderFactory : IEmployeeBenefitProviderFactory
{
    private readonly IDependentBenefitProviderFactory _dependentBenefitProviderFactory;

    public EmployeeBenefitProviderFactory(IDependentBenefitProviderFactory dependentBenefitProviderFactory)
    {
        _dependentBenefitProviderFactory = dependentBenefitProviderFactory;
    }

    public Task<EmployeeBenefitProvider> GetProvider(Db.Employee employee)
    {
        if (!string.IsNullOrEmpty(employee.Name) && employee.Name.StartsWith('A'))
        {
            return Task.FromResult((EmployeeBenefitProvider) new DiscountEmployeeBenefitProvider(employee, _dependentBenefitProviderFactory));
        }

        return Task.FromResult((EmployeeBenefitProvider) new RegularEmployeeBenefitProvider(employee, _dependentBenefitProviderFactory));
    }
}