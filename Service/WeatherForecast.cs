namespace Service;

public record WeatherForecast(DateOnly Date, string City, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}