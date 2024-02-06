# Zupit.VersionEndpoint
This library adds a version endpoint to your ASP.NET Core application. The version is based on the assembly version of the application. You can customize it as you like.

The endpoint is accessible via `GET /version` and returns a string with the version. You can customize the endpoint path.

## Prerequisites
- .NET 7 or later

## Installation
```
dotnet add package Zupit.VersionEndpoint
```

## Usage
In your `Program.cs` file, add the following code:
```csharp
using Zupit.VersionEndpoint;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

...

// Add the version endpoint
app.MapVersionEndpoint();

// or
app.MapVersionEndpoint("/custom-path");
```
