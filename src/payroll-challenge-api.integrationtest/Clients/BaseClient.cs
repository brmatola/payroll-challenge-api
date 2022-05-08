using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace payroll_challenge_api.integrationtest.Clients;

public class BaseClient
{
    protected readonly HttpClient Client;

    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        Converters = {new JsonStringEnumConverter()},
        PropertyNameCaseInsensitive = true
    };

    protected BaseClient(HttpClient client)
    {
        Client = client;
    }

    protected static async Task<(HttpStatusCode, T?)> ParseResponseAsync<T>(HttpResponseMessage response)
    {
        try
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<T>(stringResponse, Options);
            return (
                response.StatusCode,
                obj);

        }
        catch
        {
            return (response.StatusCode, default);
        }
    }
}