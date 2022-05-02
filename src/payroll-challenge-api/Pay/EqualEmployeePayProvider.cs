using payroll_challenge_api.Units;

namespace payroll_challenge_api.Pay;

public class EqualEmployeePayProvider : IEmployeePayProvider
{
    public Task<DollarsPerYear> GetPay(Guid employeeId)
    {
        var pay = new DollarsPerBiWeek(2000);
        return Task.FromResult(pay.PerYear());
    }
}