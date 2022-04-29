using payroll_challenge_api.Db;

namespace payroll_challenge_api.Benefits;

public interface IDependentBenefitProviderFactory
{
    Task<IDependentBenefitProvider> GetProvider(Dependent dependent);
}