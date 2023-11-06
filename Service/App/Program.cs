using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Service;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddOptions();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks();
services.AddSingleton<HatCoMetrics>();

services.AddOpenTelemetry()
    .ConfigureResource(x =>
    {
        x.AddDetector(new ContainerResourceDetector());
        x.AddService("Service");
    })
    .WithMetrics(x =>
    {
        x.AddMeter("HatCo.Store");
        x.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        x.AddAspNetCoreInstrumentation();
        x.AddRuntimeInstrumentation();
        x.AddProcessInstrumentation();
        x.AddPrometheusExporter();
        x.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration()
            {
                Boundaries = new[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5 },
            });
    })
    .WithTracing(x =>
    {
        x.AddAspNetCoreInstrumentation();
        x.AddConsoleExporter();
    });

var app = builder.Build();

app.Use(async (context, next) =>
{
    var tagsFeature = context.Features.Get<IHttpMetricsTagsFeature>();
    if (tagsFeature != null)
    {
        tagsFeature.Tags.Add(new KeyValuePair<string, object?>("path", context.Request.Path));
    }

    await next.Invoke();
});

app.MapPrometheusScrapingEndpoint();

app.UseHealthChecks("/healthz");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();