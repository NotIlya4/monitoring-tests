using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using OpenTelemetry.Metrics;
using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Service.Monitoring;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks();
services.AddHttpContextAccessor();

services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(x => x.InNamespaceOf<AppMetrics>())
    .AsSelf()
    .WithSingletonLifetime());

services.AddScoped<ITemperatureService, TemperatureService>();
services.Decorate<ITemperatureService, TemperatureMonitorService>();

services.AddScoped<IFeelsLikeService, FeelsLikeService>();
services.Decorate<IFeelsLikeService, FeelsLikeMonitoringService>();

services.AddScoped<IWeatherService, WeatherService>();
services.Decorate<IWeatherService, WeatherMonitorService>();

services.AddOpenTelemetry()
    .ConfigureResource(x =>
    {
        x.AddDetector(new ContainerResourceDetector());
        x.AddService("Service");
    })
    .WithMetrics(x =>
    {
        x.AddMeter(AppMetrics.MeterName);
        x.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        x.AddAspNetCoreInstrumentation();
        x.AddRuntimeInstrumentation();
        x.AddProcessInstrumentation();
        x.AddPrometheusExporter();
    })
    .WithTracing(x =>
    {
        x.AddSource(AppActivitySource.ActivitySourceName);
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