using Service.Monitoring;

namespace Service.Services;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetWeatherForecast(string city, int days);
}

public class WeatherService : IWeatherService
{
    private readonly IFeelsLikeService _feelsLikeService;
    private readonly ITemperatureService _temperatureService;

    public WeatherService(IFeelsLikeService feelsLikeService, ITemperatureService temperatureService)
    {
        _feelsLikeService = feelsLikeService;
        _temperatureService = temperatureService;
    }

    public IEnumerable<WeatherForecast> GetWeatherForecast(string city, int days)
    {
        return Enumerable.Range(1, days)
            .Select(i => GetWeather(city, DateOnly.FromDateTime(DateTime.Now).AddDays(i - 1)));
    }

    private WeatherForecast GetTodayWeather(string city)
    {
        return GetWeather(city, DateOnly.FromDateTime(DateTime.Now));
    }

    private WeatherForecast GetWeather(string city, DateOnly date)
    {
        var temperature = _temperatureService.GetTemperature(city, date);

        return new WeatherForecast(Date: date, City: city, TemperatureC: temperature,
            Summary: _feelsLikeService.HowItFeels(city, date, temperature));
    }
}

public class WeatherMonitorService : IWeatherService
{
    private readonly AppActivitySource _activitySource;
    private readonly IWeatherService _service;

    public WeatherMonitorService(AppActivitySource activitySource, IWeatherService service)
    {
        _activitySource = activitySource;
        _service = service;
    }
    
    public IEnumerable<WeatherForecast> GetWeatherForecast(string city, int days)
    {
        using var activity = _activitySource.WeatherServiceCalled(city, days);

        var weatherForecasts = _service.GetWeatherForecast(city, days);

        activity.SetTag("weatherForecasts", weatherForecasts);
        return weatherForecasts;
    }
}