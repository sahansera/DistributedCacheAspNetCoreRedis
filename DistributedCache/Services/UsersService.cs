using System.Collections.Generic;
using System.Threading.Tasks;
using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services
{
    
    public interface IUsersService
    {
        Task<IEnumerable<User>>GetUsersAsync();
    }
    
    public class UsersService : IUsersService
    {
        private readonly IHttpClient _httpClient;

        public UsersService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _httpClient.Get();
        }
    }
}