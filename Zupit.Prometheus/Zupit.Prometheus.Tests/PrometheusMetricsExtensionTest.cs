using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Zupit.Prometheus.Tests;

public class PrometheusMetricsExtensionTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PrometheusMetricsExtensionTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("token", "")]
    [InlineData("token", "aa")]
    [InlineData("aa", "aa")]
    public async Task ShouldReturnMetricsNotFoundWithoutToken(string? queryKey, string? queryValue)
    {
        // Arrange
        var client = _factory.CreateClient();

        var uri = "/metrics";
        if (queryKey is not null)
        {
			uri += $"?{queryKey}={queryValue}";
		}

        // Act
        var response = await client.GetAsync(uri);

        // Assert
        Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Empty(responseContent);
    }

    [Fact]
    public async Task ShouldReturnMetrics()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/metrics?token=cV^96k");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.StartsWith("# TYPE", responseContent);
        Assert.Contains("# EOF", responseContent);
    }
}
