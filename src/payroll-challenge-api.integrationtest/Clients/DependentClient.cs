using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using payroll_challenge_api.Dependents.Model;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.integrationtest.Clients;

public class DependentClient : BaseClient
{
    public DependentClient(HttpClient client) : base(client)
    {
    }

    public async Task<(HttpStatusCode, DependentViewModel?)> GetById(Guid dependentId)
    {
        var response = await Client.GetAsync($"/dependents/{dependentId}");
        return await ParseResponseAsync<DependentViewModel>(response);
    }
    
    public async Task<(HttpStatusCode, IEnumerable<DependentViewModel>?)> GetEmployeeDependents(Guid employeeId)
    {
        var response = await Client.GetAsync($"Employees/{employeeId}/dependents");
        return await ParseResponseAsync<IEnumerable<DependentViewModel>>(response);
    }
    
    public async Task<(HttpStatusCode, DependentViewModel?)> Create(Guid employeeId, string name)
    {
        var obj = new JsonObject();
        obj["name"] = name;
        
        var response = await Client.PostAsync($"employees/{employeeId}/dependents", 
            new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
        return await ParseResponseAsync<DependentViewModel>(response);
    }
}