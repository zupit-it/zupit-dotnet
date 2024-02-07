using Microsoft.AspNetCore.Mvc.Testing;

namespace Zupit.VersionEndpoint.Tests;

public class VersionEndpointExtensionTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public VersionEndpointExtensionTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldReturnVersion()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/version");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("15.0.0.0", responseContent);
    }
}
