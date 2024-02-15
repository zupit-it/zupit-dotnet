using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Zupit.Prometheus;

public record TokenQueryStringAuthorizationMiddlewareOptions()
{
    public const string SectionName = "Metrics";

    [Required(AllowEmptyStrings = false)]
    public string TokenKey { get; init; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string TokenValue { get; init; } = default!;
}

public class TokenQueryStringAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenQueryStringAuthorizationMiddlewareOptions _options;

    public TokenQueryStringAuthorizationMiddleware(RequestDelegate next, IOptions<TokenQueryStringAuthorizationMiddlewareOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (string.IsNullOrEmpty(_options.TokenKey) || string.IsNullOrEmpty(_options.TokenValue))
        {
            await _next(context);
            return;
        }
        if (context.Request.Query.TryGetValue(_options.TokenKey, out var token) && string.Compare(token, _options.TokenValue, StringComparison.OrdinalIgnoreCase) == 0)
        {
            await _next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status404NotFound;
    }
}
