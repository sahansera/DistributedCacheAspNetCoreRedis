using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services;

public interface ICacheUserService
{
    Task<IReadOnlyList<User>> GetCachedUserAsync();
    Task ClearCacheAsync();
}

public static class CacheKeys
{
    public const string Users = "users";
}

public class CacheUserService(ICacheProvider cacheProvider) : ICacheUserService
{
    private readonly ICacheProvider _cacheProvider = cacheProvider;

    public async Task<IReadOnlyList<User>> GetCachedUserAsync()
    {
        return await _cacheProvider.GetFromCacheAsync<IReadOnlyList<User>>(CacheKeys.Users) ?? [];
    }

    public Task ClearCacheAsync()
    {
        return _cacheProvider.ClearCacheAsync(CacheKeys.Users);
    }
}
