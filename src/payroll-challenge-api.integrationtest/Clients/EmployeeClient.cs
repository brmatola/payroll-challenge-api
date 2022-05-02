using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.integrationtest.Clients;

public class EmployeeClient : BaseClient
{
    public EmployeeClient(HttpClient client) : base(client)
    {
    }

    public async Task<(HttpStatusCode, IEnumerable<EmployeeViewModel>?)> Get()
    {
        var response = await Client.GetAsync("Employees");
        return await ParseResponseAsync<IEnumerable<EmployeeViewModel>>(response);
    }

    public async Task<(HttpStatusCode, EmployeeViewModel?)> Get(Guid id)
    {
        var response = await Client.GetAsync($"Employees/{id}");
        return await ParseResponseAsync<EmployeeViewModel>(response);
    }
    
    public async Task<(HttpStatusCode, EmployeeViewModel?)> Create(string name)
    {
        var obj = new JsonObject();
        obj["name"] = name;
        
        var response = await Client.PostAsync($"Employees/", 
            new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
        return await ParseResponseAsync<EmployeeViewModel>(response);
    }
    
    public async Task<HttpStatusCode> Delete(Guid id)
    {
        var response = await Client.DeleteAsync($"Employees/{id}");
        return response.StatusCode;
    }

    public async Task<(HttpStatusCode, BenefitCostResponse?)> GetBenefitCost(Guid id)
    {
        var response = await Client.GetAsync($"Employees/{id}/benefit_cost");
        return await ParseResponseAsync<BenefitCostResponse>(response);
    }

    public async Task<(HttpStatusCode, BenefitCostResponse?)> GetPaycheck(Guid id)
    {
        var response = await Client.GetAsync($"Employees/{id}/paycheck");
        return await ParseResponseAsync<BenefitCostResponse>(response);
    }
}
    
    