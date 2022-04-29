using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using payroll_challenge_api.integrationtest.Clients;

namespace payroll_challenge_api.integrationtest;

public class BenefitTests
{
    private DependentClient _dependentClient = new DependentClient(new HttpClient());
    private EmployeeClient _employeeClient = new EmployeeClient(new HttpClient());
    
    [SetUp]
    public void Setup()
    {
        var app = new MockWebApplicationFactory();
        _dependentClient = new DependentClient(app.CreateClient());
        _employeeClient = new EmployeeClient(app.CreateClient());
    }

    [Test]
    public async Task BenefitsReturnedForNonAName()
    {
        var (_, employee) = await _employeeClient.Create("Brad");
        if (employee == null) throw new Exception();

        var (status, cost) = await _employeeClient.GetBenefitCost(employee.Id);
        Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(cost?.DollarPerYear, Is.EqualTo(1000));
    }
    
    [Test]
    public async Task BenefitsReturnedForAName()
    {
        var (_, employee) = await _employeeClient.Create("Adam");
        if (employee == null) throw new Exception();

        var (status, cost) = await _employeeClient.GetBenefitCost(employee.Id);
        Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(cost?.DollarPerYear, Is.EqualTo(900));
    }
}