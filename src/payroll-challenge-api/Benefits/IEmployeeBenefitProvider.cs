using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits;

public interface IEmployeeBenefitProvider
{
    Task<DollarsPerYear> GetBenefitCost();
}