# Zupit.Prometheus
This library adds an endpoint to your ASP.NET Core application that returns metrics in the Prometheus format with a token-based authentication.
The endpoint is accessible via `GET /metrics`

## Prerequisites
- .NET 7 or later

## Installation
```
dotnet add package Zupit.Prometheus
```

## Configuration
In your `appsettings.json` file, add the following configuration:
```json
{
 "Metrics": {
  "TokenKey": "your-query-param-token-key",
  "TokenValue": "your-token-value"
 }
}
```
For example:
```json
{
 "Metrics": {
  "TokenKey": "token",
  "TokenValue": "cV^96k"
 }
}
```
configures the endpoint to be accessible via `GET /metrics?token=cV^96k`.
If the token is not provided or is incorrect, the endpoint will return a `404 Not Found` response.

## Usage
In your `Program.cs` file, add the following code:
```csharp
using Zupit.Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add Prometheus metric services (with configuration validation on start)
builder.Services.AddPrometheusMetrics(builder.Configuration);

...

var app = builder.Build();

...

// Add the Prometheus endpoint with token auth
app.MapPrometheusEndpointWithToken();
```
