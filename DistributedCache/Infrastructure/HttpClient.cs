using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DistributedCache.Models;

namespace DistributedCache.Infrastructure
{
    
    public class UserResponse
    {
        [JsonPropertyName("data")]
        public User[] Data { get; set; }
    }
    
    public interface IHttpClient
    {
        Task<IEnumerable<User>> Get();
    }
    
    public class HttpClient : IHttpClient
    {
        private const string UsersEndpoint = "https://reqres.in/api/users";
        private readonly IHttpClientFactory _clientFactory;

        public HttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<User>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, UsersEndpoint);
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                var usersResponse = await JsonSerializer.DeserializeAsync<UserResponse>(responseStream);
                var users = usersResponse?.Data;
                return users;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}