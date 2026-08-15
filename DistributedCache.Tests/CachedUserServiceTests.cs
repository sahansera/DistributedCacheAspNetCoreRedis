using DistributedCache.Infrastructure;
using DistributedCache.Models;
using DistributedCache.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace DistributedCache.Tests;

public class CachedUserServiceTests
{
    private static readonly IReadOnlyList<User> Users =
        [new User { Id = 1, Email = "leanne@example.com" }];

    [Fact]
    public async Task GetUsersAsync_WhenCacheExists_DoesNotCallUsersApi()
    {
        var usersApi = new Mock<IUsersApiClient>();
        var cache = new Mock<ICacheProvider>();
        cache.Setup(x => x.GetFromCacheAsync<IReadOnlyList<User>>(CacheKeys.Users))
            .ReturnsAsync(Users);
        var service = new CachedUserService(new UserService(usersApi.Object), cache.Object);

        var result = await service.GetUsersAsync();

        Assert.Same(Users, result);
        usersApi.Verify(x => x.GetUsersAsync(), Times.Never);
    }

    [Fact]
    public async Task GetUsersAsync_WhenCacheIsEmpty_LoadsAndCachesUsers()
    {
        var usersApi = new Mock<IUsersApiClient>();
        usersApi.Setup(x => x.GetUsersAsync()).ReturnsAsync(Users);
        var cache = new Mock<ICacheProvider>();
        cache.Setup(x => x.GetFromCacheAsync<IReadOnlyList<User>>(CacheKeys.Users))
            .ReturnsAsync((IReadOnlyList<User>?)null);
        var service = new CachedUserService(new UserService(usersApi.Object), cache.Object);

        var result = await service.GetUsersAsync();

        Assert.Same(Users, result);
        usersApi.Verify(x => x.GetUsersAsync(), Times.Once);
        cache.Verify(x => x.SetCacheAsync(
            CacheKeys.Users,
            Users,
            It.IsAny<DistributedCacheEntryOptions>()), Times.Once);
    }
}
