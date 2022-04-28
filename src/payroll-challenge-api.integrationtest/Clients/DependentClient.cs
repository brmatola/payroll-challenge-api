using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using payroll_challenge_api.Dependents.Model;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.integrationtest.Clients;

public class DependentClient : BaseClient
{
    public DependentClient(HttpClient client) : base(client)
    {
    }
    
    public async Task<(HttpStatusCode, IEnumerable<DependentViewModel>?)> Get(Guid employeeId)
    {
        var response = await Client.GetAsync($"Employees/{employeeId}/dependents");
        return await ParseResponseAsync<IEnumerable<DependentViewModel>>(response);
    }
}