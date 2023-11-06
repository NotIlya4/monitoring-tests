using System.Diagnostics.Metrics;
using Service.Services;

namespace Service.Monitoring;

public class AppMetrics
{
    public const string MeterName = "AppMeter";
    public const string TemperatureServiceCalledMetricName = "temperature_service.call.duration";
    public const string FeelsLikeServiceCalledMetricName = "feels_like_service.call.duration";
    
    public Histogram<double> TemperatureServiceCallDuration { get; set; }
    public Histogram<double> FeelsLikeServiceCallDuration { get; set; }
    
    public AppMetrics(IMeterFactory factory)
    {
        var meter = factory.Create(AppMetrics.MeterName);
        TemperatureServiceCallDuration = meter.CreateHistogram<double>(TemperatureServiceCalledMetricName, "s",
            $"Tracks duration of {nameof(TemperatureService)} call", new []{new KeyValuePair<string, object?>("pisya", "popa")});
        FeelsLikeServiceCallDuration = meter.CreateHistogram<double>(FeelsLikeServiceCalledMetricName, "s",
            $"Tracks duration of {nameof(FeelsLikeService)} call"); 
    }
}