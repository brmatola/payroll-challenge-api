using System;
using NUnit.Framework;
using payroll_challenge_api.Units;

namespace payroll_challenge_api.test.Units;

public class DollarsPerYearTests
{
    [TestCase(50, 50, 100)]
    [TestCase(50, 0, 50)]
    [TestCase(0, 50, 50)]
    [TestCase(0, 0, 0)]
    public void CanAddAmounts(double first, double second, double expected)
    {
        var a = new DollarsPerYear(first);
        var b = new DollarsPerYear(second);

        var resp = a + b;
        
        Assert.That(resp.Value, Is.EqualTo(expected));
    }

    [TestCase(100, 50, 50)]
    [TestCase(100, 100, 0)]
    public void CanSubtractAmounts(double first, double second, double expected)
    {
        var a = new DollarsPerYear(first);
        var b = new DollarsPerYear(second);

        var resp = a - b;
        
        Assert.That(resp.Value, Is.EqualTo(expected));
    }

    [Test]
    public void ThrowsIfNegative()
    {
        var a = new DollarsPerYear(25);
        var b = new DollarsPerYear(50);

        Assert.Throws<ArgumentException>(() =>
        {
            var newAmount = a - b;
        });
    }

    [TestCase(26, 1)]
    [TestCase(100, 3.84)]
    [TestCase(0, 0)]
    public void CanConvertToBiWeek(double amount, double expected)
    {
        var perYear = new DollarsPerYear(amount);
        var perBiWeek = (DollarsPerBiWeek) perYear;
        
        Assert.That(perBiWeek.Value, Is.EqualTo(expected));
    }
}