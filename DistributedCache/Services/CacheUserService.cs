using System.Collections.Generic;
using System.Threading.Tasks;
using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services
{
    
    public interface ICacheUserService
    {
        Task<IEnumerable<User>> GetCachedUser();
        Task ClearCache();
    }

    public static class CacheKeys
    {
        public static string Users => "_Users";
    }
    
    public class CacheUserService : ICacheUserService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheUserService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<IEnumerable<User>> GetCachedUser()
        {
            return await _cacheProvider.GetFromCache<IEnumerable<User>>(CacheKeys.Users);
        }

        public async Task ClearCache()
        {
            await _cacheProvider.ClearCache(CacheKeys.Users);
        }
    }
}