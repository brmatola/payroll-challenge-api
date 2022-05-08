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
    private readonly IPayConverter _payConverter;

    public EmployeeBenefitService(
        IEmployeeRepository employeeRepository,
        IEmployeeBenefitProviderFactory employeeBenefitProviderFactory,
        IEmployeePayProvider employeePayProvider,
        IPayConverter payConverter)
    {
        _employeeRepository = employeeRepository;
        _employeeBenefitProviderFactory = employeeBenefitProviderFactory;
        _employeePayProvider = employeePayProvider;
        _payConverter = payConverter;
    }

    
    public async Task<BenefitCostResponse> GetBenefitCost(Guid employeeId, TimePeriod timePeriod)
    {
        var cost = await GetBenefitCostPrivate(employeeId);

        return new BenefitCostResponse
        {
            Amount = await _payConverter.Convert(employeeId, cost, timePeriod),
            TimePeriod = timePeriod
        };
    }

    public async Task<BenefitCostResponse> GetPaycheck(Guid employeeId, TimePeriod timePeriod)
    {
        var benefitCost = await GetBenefitCostPrivate(employeeId);
        var pay = await _employeePayProvider.GetPay(employeeId);

        var paycheck = pay - benefitCost;

        return new BenefitCostResponse
        {
            Amount = await _payConverter.Convert(employeeId, paycheck, timePeriod),
            TimePeriod = timePeriod
        };
    }

    private async Task<DollarsPerYear> GetBenefitCostPrivate(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        var provider = await _employeeBenefitProviderFactory.GetProvider(employee);
        return await provider.GetBenefitCost();
    }
}