using System.Net.Http.Json;
using DistributedCache.Models;

namespace DistributedCache.Infrastructure;

public interface IUsersApiClient
{
    Task<IReadOnlyList<User>> GetUsersAsync();
}

public class UsersApiClient(IHttpClientFactory clientFactory) : IUsersApiClient
{
    private const string UsersEndpoint = "https://jsonplaceholder.typicode.com/users";
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public async Task<IReadOnlyList<User>> GetUsersAsync()
    {
        var client = _clientFactory.CreateClient();
        return await client.GetFromJsonAsync<User[]>(UsersEndpoint) ?? [];
    }
}
