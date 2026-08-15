using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCache.Infrastructure;

public interface ICacheProvider
{
    Task<T?> GetFromCacheAsync<T>(string key) where T : class;
    Task SetCacheAsync<T>(string key, T value, DistributedCacheEntryOptions options)
        where T : class;
    Task ClearCacheAsync(string key);
}

public class CacheProvider(IDistributedCache cache) : ICacheProvider
{
    private readonly IDistributedCache _cache = cache;

    public async Task<T?> GetFromCacheAsync<T>(string key) where T : class
    {
        var cachedValue = await _cache.GetStringAsync(key);
        return cachedValue is null ? null : JsonSerializer.Deserialize<T>(cachedValue);
    }

    public Task SetCacheAsync<T>(
        string key,
        T value,
        DistributedCacheEntryOptions options) where T : class
    {
        var serializedValue = JsonSerializer.Serialize(value);
        return _cache.SetStringAsync(key, serializedValue, options);
    }

    public Task ClearCacheAsync(string key)
    {
        return _cache.RemoveAsync(key);
    }
}
