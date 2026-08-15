using System.Text;
using System.Text.Json;
using DistributedCache.Infrastructure;
using DistributedCache.Models;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace DistributedCache.Tests;

public class CacheProviderTests
{
    [Fact]
    public async Task GetFromCacheAsync_WhenKeyExists_ReturnsDeserializedValue()
    {
        var users = new[] { new User { Id = 1, Email = "leanne@example.com" } };
        var serializedUsers = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(users));
        var cache = new Mock<IDistributedCache>();
        cache.Setup(x => x.GetAsync("users", It.IsAny<CancellationToken>()))
            .ReturnsAsync(serializedUsers);
        var provider = new CacheProvider(cache.Object);

        var result = await provider.GetFromCacheAsync<IReadOnlyList<User>>("users");

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("leanne@example.com", result[0].Email);
    }

    [Fact]
    public async Task SetCacheAsync_SerializesAndStoresValue()
    {
        var cache = new Mock<IDistributedCache>();
        var provider = new CacheProvider(cache.Object);
        var users = new[] { new User { Id = 1, Email = "leanne@example.com" } };

        await provider.SetCacheAsync("users", users, new DistributedCacheEntryOptions());

        cache.Verify(x => x.SetAsync(
            "users",
            It.Is<byte[]>(value => Encoding.UTF8.GetString(value).Contains("leanne@example.com")),
            It.IsAny<DistributedCacheEntryOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ClearCacheAsync_RemovesKey()
    {
        var cache = new Mock<IDistributedCache>();
        var provider = new CacheProvider(cache.Object);

        await provider.ClearCacheAsync("users");

        cache.Verify(x => x.RemoveAsync("users", It.IsAny<CancellationToken>()), Times.Once);
    }
}
