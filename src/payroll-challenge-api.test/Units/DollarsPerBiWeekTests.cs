using NUnit.Framework;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.test.Units;

public class DollarsPerBiWeekTests
{
    [Test]
    public void DoesConvertToDollarsPerYear()
    {
        var pay = new DollarsPerBiWeek(1000);
        Assert.That(pay.PerYear().Value, Is.EqualTo(26000));
    }
}