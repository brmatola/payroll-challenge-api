using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits.Dependents;

public class DiscountDependentBenefitProvider : IDependentBenefitProvider
{
    public Task<DollarsPerYear> GetBenefitCost()
    {
        return Task.FromResult(new DollarsPerYear(450));
    }
}