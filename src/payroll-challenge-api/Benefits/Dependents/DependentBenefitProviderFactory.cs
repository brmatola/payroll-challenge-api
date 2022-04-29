using payroll_challenge_api.Db;

namespace payroll_challenge_api.Benefits.Dependents;

public class DependentBenefitProviderFactory : IDependentBenefitProviderFactory
{
    public Task<IDependentBenefitProvider> GetProvider(Dependent dependent)
    {
        if (!string.IsNullOrEmpty(dependent.Name) && dependent.Name.StartsWith('A'))
        {
            return Task.FromResult((IDependentBenefitProvider) new DiscountDependentBenefitProvider());
        }

        return Task.FromResult((IDependentBenefitProvider) new RegularDependentBenefitProvider());
    }
}