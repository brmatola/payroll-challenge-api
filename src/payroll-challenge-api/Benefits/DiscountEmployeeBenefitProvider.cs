using payroll_challenge_api.Units;

namespace payroll_challenge_api.Benefits;

public class DiscountEmployeeBenefitProvider : IEmployeeBenefitProvider
{
    public Task<DollarsPerYear> GetBenefitCost()
    {
        return Task.FromResult(new DollarsPerYear(900));
    }
}