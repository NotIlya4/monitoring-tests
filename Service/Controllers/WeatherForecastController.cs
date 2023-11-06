using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Service.Controllers;

[ApiController]
[Route("weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _service;

    public WeatherForecastController(IWeatherService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> Get(string city, int days = 100)
    {
        return Ok(_service.GetWeatherForecast(city, days));
    }
}