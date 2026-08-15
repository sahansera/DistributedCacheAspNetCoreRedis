using DistributedCache.Infrastructure;
using DistributedCache.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCache.Services;

public class CachedUserService(
    UserService userService,
    ICacheProvider cacheProvider) : IUserService
{
    private const int CacheTimeToLiveInSeconds = 120;
    private readonly UserService _userService = userService;
    private readonly ICacheProvider _cacheProvider = cacheProvider;
    private static readonly SemaphoreSlim GetUsersSemaphore = new(1, 1);

    public Task<IReadOnlyList<User>> GetUsersAsync()
    {
        return GetCachedResponseAsync(
            CacheKeys.Users,
            GetUsersSemaphore,
            _userService.GetUsersAsync);
    }

    private async Task<IReadOnlyList<User>> GetCachedResponseAsync(
        string cacheKey,
        SemaphoreSlim semaphore,
        Func<Task<IReadOnlyList<User>>> valueFactory)
    {
        var users = await _cacheProvider.GetFromCacheAsync<IReadOnlyList<User>>(cacheKey);
        if (users is not null) return users;

        await semaphore.WaitAsync();
        try
        {
            users = await _cacheProvider.GetFromCacheAsync<IReadOnlyList<User>>(cacheKey);
            if (users is not null) return users;

            users = await valueFactory();
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(CacheTimeToLiveInSeconds));
            await _cacheProvider.SetCacheAsync(cacheKey, users, cacheEntryOptions);

            return users;
        }
        finally
        {
            semaphore.Release();
        }
    }
}
