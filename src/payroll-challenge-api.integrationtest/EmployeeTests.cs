using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using payroll_challenge_api.integrationtest.Clients;

namespace payroll_challenge_api.integrationtest;

public class EmployeeTests
{
    private EmployeeClient _client;
    [SetUp]
    public void Setup()
    {
        var app = new MockWebApplicationFactory();
        _client = new EmployeeClient(app.CreateClient());
    }
    
    [Test]
    public async Task CanGetSeededEmployees()
    {
        var (statusCode, results) = await _client.Get();
        
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(results?.Count(), Is.EqualTo(5));
    }

    [Test]
    public async Task CanGetEmployeeDetails()
    {
        var (_, results) = await _client.Get();
        var first = results?.First() ?? throw new Exception("No results");

        var (statusCode, details) = await _client.Get(first.Id);
        
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(details?.Id, Is.EqualTo(first.Id));
        Assert.That(details?.Name, Is.EqualTo(first.Name));
    }

    [Test]
    public async Task RandomGuidReturnsNotFound()
    {
        var (statusCode, _) = await _client.Get(Guid.NewGuid());
        
        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CanCreateNewEmployee()
    {
        var (statusCode, model) = await _client.Create("Jim");

        Assert.That(statusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(model?.Name, Is.EqualTo("Jim"));

        var (statusCode2, details) = await _client.Get(model.Id);
        Assert.That(statusCode2, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(details?.Name, Is.EqualTo("Jim"));

        var (statusCode3, results) = await _client.Get();
        Assert.That(statusCode3, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(results?.Count(), Is.EqualTo(6));
    }
}