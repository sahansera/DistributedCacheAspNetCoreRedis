using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services;

public interface IUserService
{
    Task<IReadOnlyList<User>> GetUsersAsync();
}

public class UserService(IUsersApiClient usersApiClient) : IUserService
{
    private readonly IUsersApiClient _usersApiClient = usersApiClient;

    public Task<IReadOnlyList<User>> GetUsersAsync()
    {
        return _usersApiClient.GetUsersAsync();
    }
}
