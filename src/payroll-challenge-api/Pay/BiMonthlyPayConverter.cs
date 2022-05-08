using payroll_challenge_api.Employees.Model;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Pay;

public class BiMonthlyPayConverter : IPayConverter
{
    public Task<double> Convert(Guid employeeId, DollarsPerYear amount, TimePeriod timePeriod)
    {
        switch (timePeriod)
        {
            case TimePeriod.PerYear:
                return Task.FromResult(amount.Value);
            case TimePeriod.PerPaycheck:
                var perBiWeek = (DollarsPerBiWeek) amount;
                return Task.FromResult(perBiWeek.Value);
            default:
                throw new ArgumentOutOfRangeException(nameof(timePeriod), timePeriod, null);
        }
    }
}