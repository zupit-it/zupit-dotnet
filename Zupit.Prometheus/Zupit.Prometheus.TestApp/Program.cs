using Zupit.Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddPrometheusMetrics(builder.Configuration);

var app = builder.Build();

app.MapPrometheusEndpointWithToken();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }