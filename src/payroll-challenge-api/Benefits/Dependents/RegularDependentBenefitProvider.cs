using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits.Dependents;

public class RegularDependentBenefitProvider : IDependentBenefitProvider
{
    public Task<DollarsPerYear> GetBenefitCost()
    {
        return Task.FromResult(new DollarsPerYear(500));
    }
}