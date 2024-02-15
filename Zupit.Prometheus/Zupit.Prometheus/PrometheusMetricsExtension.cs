using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;

namespace Zupit.Prometheus;

public static class PrometheusMetricsExtension
{
    public static void AddPrometheusMetrics(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(meterBuilder =>
            {
                meterBuilder.AddAspNetCoreInstrumentation();
                meterBuilder.AddPrometheusExporter();
            });

        var configurationSection = configuration.GetSection(TokenQueryStringAuthorizationMiddlewareOptions.SectionName);
        services.AddOptions<TokenQueryStringAuthorizationMiddlewareOptions>().Bind(configurationSection).ValidateDataAnnotations().ValidateOnStart();
    }

    public static IEndpointConventionBuilder MapPrometheusEndpointWithToken(this WebApplication app)
    {
        var tokenOption = app.Services.GetRequiredService<IOptions<TokenQueryStringAuthorizationMiddlewareOptions>>();

        app.UseWhen(
            context => context.Request.Path.Value?.StartsWith("/metrics", StringComparison.OrdinalIgnoreCase) ?? false,
            appBuilder =>
            {
                appBuilder.UseMiddleware<TokenQueryStringAuthorizationMiddleware>(tokenOption);
            }
        );

        return app.MapPrometheusScrapingEndpoint();
    }
}
