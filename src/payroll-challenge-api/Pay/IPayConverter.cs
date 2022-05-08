using payroll_challenge_api.Employees.Model;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.Pay;

public interface IPayConverter
{
    Task<double> Convert(Guid employeeId, DollarsPerYear amount, TimePeriod timePeriod);
}