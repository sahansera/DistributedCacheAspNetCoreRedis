using System.Collections.Generic;
using System.Threading.Tasks;
using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services
{
    
    public interface ICacheService
    {
        Task<IEnumerable<User>> GetCachedUser();
        void ClearCache();
    }

    public static class CacheKeys
    {
        public static string Users => "_Users";
    }
    
    public class CacheService : ICacheService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<IEnumerable<User>> GetCachedUser()
        {
            return await _cacheProvider.GetFromCache<IEnumerable<User>>(CacheKeys.Users);
        }

        public void ClearCache()
        {
            _cacheProvider.ClearCache(CacheKeys.Users);
        }
    }
}