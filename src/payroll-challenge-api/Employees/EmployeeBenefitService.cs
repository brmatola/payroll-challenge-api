using payroll_challenge_api.Benefits;
using payroll_challenge_api.Db;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

public class EmployeeBenefitService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBenefitProviderFactory _employeeBenefitProviderFactory;

    public EmployeeBenefitService(
        IEmployeeRepository employeeRepository,
        IEmployeeBenefitProviderFactory employeeBenefitProviderFactory)
    {
        _employeeRepository = employeeRepository;
        _employeeBenefitProviderFactory = employeeBenefitProviderFactory;
    }

    public async Task<BenefitCostResponse> GetBenefitCost(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        var provider = await _employeeBenefitProviderFactory.GetProvider(employee);
        var cost = await provider.GetBenefitCost();

        return new BenefitCostResponse
        {
            DollarPerYear = cost.Value
        };
    }
}