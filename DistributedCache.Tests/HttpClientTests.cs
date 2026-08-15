using System.Net;
using DistributedCache.Infrastructure;
using Moq;
using Moq.Protected;

namespace DistributedCache.Tests;

public class HttpClientTests
{
    [Fact]
    public async Task GetUsersAsync_ReturnsUsersFromApi()
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    """[{"id":1,"email":"leanne@example.com"}]""")
            });
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new System.Net.Http.HttpClient(handler.Object));
        var client = new UsersApiClient(factory.Object);

        var result = await client.GetUsersAsync();

        Assert.Single(result);
        Assert.Equal("leanne@example.com", result[0].Email);
    }

    [Fact]
    public async Task GetUsersAsync_WhenApiReturnsError_ThrowsHttpRequestException()
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new System.Net.Http.HttpClient(handler.Object));
        var client = new UsersApiClient(factory.Object);

        await Assert.ThrowsAsync<HttpRequestException>(() => client.GetUsersAsync());
    }
}
