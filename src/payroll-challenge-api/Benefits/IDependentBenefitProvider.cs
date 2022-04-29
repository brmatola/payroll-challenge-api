using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits;

public interface IDependentBenefitProvider
{
    Task<DollarsPerYear> GetBenefitCost();
}