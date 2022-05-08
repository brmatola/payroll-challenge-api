using payroll_challenge_api.Benefits;
using payroll_challenge_api.Db;
using payroll_challenge_api.Employees.Model;
using payroll_challenge_api.Pay;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Employees;

public class EmployeeBenefitService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBenefitProviderFactory _employeeBenefitProviderFactory;
    private readonly IEmployeePayProvider _employeePayProvider;

    public EmployeeBenefitService(
        IEmployeeRepository employeeRepository,
        IEmployeeBenefitProviderFactory employeeBenefitProviderFactory,
        IEmployeePayProvider employeePayProvider)
    {
        _employeeRepository = employeeRepository;
        _employeeBenefitProviderFactory = employeeBenefitProviderFactory;
        _employeePayProvider = employeePayProvider;
    }

    
    public async Task<BenefitCostResponse> GetBenefitCost(Guid employeeId)
    {
        var cost = await GetBenefitCostPrivate(employeeId);

        return new BenefitCostResponse
        {
            Amount = cost.Value,
            TimePeriod = TimePeriod.PerYear
        };
    }

    public async Task<BenefitCostResponse> GetPaycheck(Guid employeeId)
    {
        var benefitCost = await GetBenefitCostPrivate(employeeId);
        var pay = await _employeePayProvider.GetPay(employeeId);

        var paycheck = pay - benefitCost;

        return new BenefitCostResponse
        {
            Amount = paycheck.Value,
            TimePeriod = TimePeriod.PerYear
        };
    }

    private async Task<DollarsPerYear> GetBenefitCostPrivate(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        var provider = await _employeeBenefitProviderFactory.GetProvider(employee);
        return await provider.GetBenefitCost();
    }
}