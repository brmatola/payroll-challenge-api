using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using payroll_challenge_api.integrationtest.Clients;

namespace payroll_challenge_api.integrationtest;

public class DependentTests
{
    private DependentClient _dependentClient;
    private EmployeeClient _employeeClient;
    
    [SetUp]
    public void Setup()
    {
        var app = new MockWebApplicationFactory();
        _dependentClient = new DependentClient(app.CreateClient());
        _employeeClient = new EmployeeClient(app.CreateClient());
    }

    [Test]
    public async Task CanGetSeedDependents()
    {
        var (_, employees) = await _employeeClient.Get();
        var peter = employees.Single(x => x.Name == "Peter");

        var (peterStatus, peterDependents) = await _dependentClient.GetEmployeeDependents(peter.Id);
        Assert.That(peterStatus, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(peterDependents?.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task CanAddADependent()
    {
        var (_, employee) = await _employeeClient.Create("Larry");
        var (status, details) = await _dependentClient.Create(employee.Id, "Linda");

        Assert.That(status, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(details.Name, Is.EqualTo("Linda"));

        var (_, dependents) = await _dependentClient.GetEmployeeDependents(employee.Id);
        Assert.That(dependents?.Count(), Is.EqualTo(1));

        var (statusCode, dependent) = await _dependentClient.GetById(details.Id);
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(dependent?.Name, Is.EqualTo("Linda"));
        Assert.That(dependent?.Id, Is.EqualTo(details.Id));
    }

    [Test]
    public async Task UnknownDependentIsNotFound()
    {
        var (statusCode, _) = await _dependentClient.GetById(Guid.NewGuid());
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CanDeleteDependent()
    {
        var (_, employee) = await _employeeClient.Create("Steve");
        var (_, dependent) = await _dependentClient.Create(employee.Id, "Jim");

        var statusCode = await _dependentClient.Delete(dependent.Id);
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));

        var (getCode, _) = await _dependentClient.GetById(dependent.Id);
        Assert.That(getCode, Is.EqualTo(HttpStatusCode.NotFound));

        var (_, results) = await _dependentClient.GetEmployeeDependents(employee.Id);
        Assert.That(results?.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task DeletingEmployeeDoesDeleteDependents()
    {
        var (_, employee) = await _employeeClient.Create("Steve");
        var (_, dependent) = await _dependentClient.Create(employee.Id, "Jim");

        var code = await _employeeClient.Delete(employee.Id);
        Assert.That(code, Is.EqualTo(HttpStatusCode.OK));

        var (getCode, _) = await _dependentClient.GetById(dependent.Id);
        Assert.That(getCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CannotCreateDependentWithEmptyName()
    {
        var (_, employee) = await _employeeClient.Create("Brad");
        var (status, _) = await _dependentClient.Create(employee.Id, "");
        
        Assert.That(status, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}