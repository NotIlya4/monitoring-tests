using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("weather")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly HatCoMetrics _metrics;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, HatCoMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get(int number = 100)
    {
        _metrics.HatsSold(2);
        
        return Enumerable.Range(1, number).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}