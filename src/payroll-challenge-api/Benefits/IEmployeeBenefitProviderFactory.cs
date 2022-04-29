using payroll_challenge_api.Db;

namespace payroll_challenge_api.Benefits;

public interface IEmployeeBenefitProviderFactory
{
    Task<EmployeeBenefitProvider> GetProvider(Employee employee);
}