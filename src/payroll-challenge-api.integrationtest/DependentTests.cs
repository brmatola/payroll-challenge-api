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

        var (peterStatus, peterDependents) = await _dependentClient.Get(peter.Id);
        Assert.That(peterStatus, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(peterDependents?.Count(), Is.EqualTo(0));
    }
}