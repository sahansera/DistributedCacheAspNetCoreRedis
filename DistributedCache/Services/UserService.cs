using System.Collections.Generic;
using System.Threading.Tasks;
using DistributedCache.Infrastructure;
using DistributedCache.Models;

namespace DistributedCache.Services
{
    
    public interface IUserService
    {
        Task<IEnumerable<User>>GetUsersAsync();
    }
    
    public class UserService : IUserService
    {
        private readonly IHttpClient _httpClient;

        public UserService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _httpClient.Get();
        }
    }
}