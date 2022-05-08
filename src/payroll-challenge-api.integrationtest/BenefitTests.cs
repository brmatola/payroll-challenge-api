using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using payroll_challenge_api.Employees.Model;
using payroll_challenge_api.integrationtest.Clients;

namespace payroll_challenge_api.integrationtest;

public class BenefitTests
{
    private DependentClient _dependentClient = new(new HttpClient());
    private EmployeeClient _employeeClient = new(new HttpClient());
    
    [SetUp]
    public void Setup()
    {
        var app = new MockWebApplicationFactory();
        _dependentClient = new DependentClient(app.CreateClient());
        _employeeClient = new EmployeeClient(app.CreateClient());
    }

    [TestCaseSource(typeof(BenefitTestCases))]
    public async Task BenefitsAreCorrect(BenefitTestCase data)
    {
        var (_, employee) = await _employeeClient.Create(data.EmployeeName);
        if (employee == null) throw new Exception("No employee returned!");

        foreach (var dependent in data.DependentNames)
        {
            await _dependentClient.Create(employee.Id, dependent);
        }

        var (status, cost) = await _employeeClient.GetBenefitCost(employee.Id);
        
        Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(cost?.Amount, Is.EqualTo(data.ExpectedBenefitCost));
        Assert.That(cost?.TimePeriod, Is.EqualTo(TimePeriod.PerYear));

        var (payStatus, paycheck) = await _employeeClient.GetPaycheck(employee.Id);

        Assert.That(payStatus, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(paycheck?.Amount, Is.EqualTo(data.ExpectedPaycheck));
        Assert.That(paycheck?.TimePeriod, Is.EqualTo(TimePeriod.PerYear));
        
        await _employeeClient.Delete(employee.Id);
    }

    public class BenefitTestCase
    {
        public string EmployeeName { get; init; } = "";
        public List<string> DependentNames { get; init; } = new();
        public double ExpectedBenefitCost { get; init; }
        public double ExpectedPaycheck { get; init; }
    }

    private class BenefitTestCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new BenefitTestCase
            {
                EmployeeName = "Brad",
                DependentNames = new List<string>(),
                ExpectedBenefitCost = 1000,
                ExpectedPaycheck = 51000
            };
            yield return new BenefitTestCase
            {
                EmployeeName = "Adam",
                DependentNames = new List<string>(),
                ExpectedBenefitCost = 900,
                ExpectedPaycheck = 51100
            };
            yield return new BenefitTestCase
            {
                EmployeeName = "Brad",
                DependentNames = new List<string> { "Noodle" },
                ExpectedBenefitCost = 1500,
                ExpectedPaycheck = 50500
            };
            yield return new BenefitTestCase
            {
                EmployeeName = "Brad",
                DependentNames = new List<string> { "Noodle", "Andrew" },
                ExpectedBenefitCost = 1950,
                ExpectedPaycheck = 50050
            };
        }
    }
}