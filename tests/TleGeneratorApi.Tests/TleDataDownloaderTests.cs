using Moq;
using Moq.Protected;
using System.Net;
using TleGeneratorApi.CatalogProviders;

namespace TleGeneratorApi.Tests;

public class TleDataDownloaderTests
{
    [Fact]
    public async Task GetTleData_CallsHttpClientWithCorrectUri()
    {
        var groupName = "weather";
        var expectedUri = new Uri($"https://celestrak.com/NORAD/elements/gp.php?GROUP={groupName}&FORMAT=TLE");

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Fake TLE Data")
            })
            .Verifiable();

        var httpClient = new HttpClient(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://celestrak.com/NORAD/elements/gp.php")
        };

        var client = new CelestrackClient(httpClient);

        var result = await client.GetTleData(groupName);

        httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get &&
                req.RequestUri == expectedUri
            ),
            ItExpr.IsAny<CancellationToken>()
        );

        Assert.Equal("Fake TLE Data", result);
    }
}
