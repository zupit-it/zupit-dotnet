using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Zupit.VersionEndpoint;

public static class VersionEndpointExtension
{
    public static IEndpointConventionBuilder MapVersionEndpoint(this IEndpointRouteBuilder endpoints, string pattern = "/version")
    {
        var versionName = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
        return endpoints.MapGet(pattern, () => versionName).WithTags("Version");
    }
}
