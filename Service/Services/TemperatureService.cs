using Service.Monitoring;

namespace Service.Services;

public interface ITemperatureService
{
    int GetTemperature(string city, DateOnly date);
}

public class TemperatureService : ITemperatureService
{
    public int GetTemperature(string city, DateOnly date)
    {
        return Random.Shared.Next(-20, 55);
    }
}

public class TemperatureMonitorService : ITemperatureService
{
    private readonly ITemperatureService _service;
    private readonly AppActivitySource _activitySource;
    private readonly AppMetrics _metrics;

    public TemperatureMonitorService(ITemperatureService service, AppActivitySource activitySource, AppMetrics metrics)
    {
        _service = service;
        _activitySource = activitySource;
        _metrics = metrics;
    }
    
    public int GetTemperature(string city, DateOnly date)
    {
        using var activity = _activitySource.TemperatureServiceCalled(city, date);
        
        var temperature = _service.GetTemperature(city, date);
        
        activity.SetTag("temperature", temperature);
        return temperature;
    }
}