using NUnit.Framework;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.test.Units;

public class DollarsPerYearTests
{
    [TestCase(50, 50, 100)]
    public void CanAddAmounts(double first, double second, double expected)
    {
        var a = new DollarsPerYear(first);
        var b = new DollarsPerYear(second);

        var resp = a + b;
        
        Assert.That(resp.Value, Is.EqualTo(expected));
    }
}