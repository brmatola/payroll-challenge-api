using payroll_challenge_api.Units;

namespace payroll_challenge_api.Pay;

public interface IEmployeePayProvider
{
    Task<DollarsPerYear> GetPay(Guid employeeId);
}