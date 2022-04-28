using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.integrationtest;

public class EmployeeTests
{
    [Test]
    public async Task FirstTest()
    {
        var app = new MockWebApplicationFactory();
        var client = app.CreateClient();

        var resp = await client.GetAsync("/employees");
        
        Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var results = await resp.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
        Assert.That(results?.Count(), Is.EqualTo(5));
    }
}