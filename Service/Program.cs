using System.Diagnostics;
using OpenTelemetry.Metrics;
using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks();

services.AddOpenTelemetry()
    .ConfigureResource(x =>
    {
        x.AddDetector(new ContainerResourceDetector());
        x.AddService("Service");
    })
    .WithMetrics(x =>
    {
        x.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        x.AddAspNetCoreInstrumentation(options =>
        {
            options.Enrich = (string name, HttpContext context, ref TagList tags) =>
            {
                tags.Add("path", context.Request.Path);
            };
        });
        x.AddRuntimeInstrumentation();
        x.AddProcessInstrumentation();
        x.AddPrometheusExporter();
    })
    .WithTracing(x =>
    {
        x.AddAspNetCoreInstrumentation();
        x.AddConsoleExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();

app.UseHealthChecks("/healthz");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();