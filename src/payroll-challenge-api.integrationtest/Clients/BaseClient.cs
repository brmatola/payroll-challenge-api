using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace payroll_challenge_api.integrationtest.Clients;

public class BaseClient
{
    protected readonly HttpClient Client;

    protected BaseClient(HttpClient client)
    {
        Client = client;
    }

    protected async Task<(HttpStatusCode, T?)> ParseResponseAsync<T>(HttpResponseMessage response)
    {
        try
        {
            return (
                response.StatusCode,
                await response.Content.ReadFromJsonAsync<T>());

        }
        catch
        {
            return (response.StatusCode, default);
        }
    }
}