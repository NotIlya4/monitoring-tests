using System.Diagnostics;

namespace Service.Monitoring;

public class AppActivitySource
{
    public const string ActivitySourceName = "AppActivitySource";
    
    public ActivitySource ActivitySource { get; set; }
    
    public AppActivitySource()
    {
        ActivitySource = new ActivitySource(ActivitySourceName);
    }

    public Activity TemperatureServiceCalled(string city, DateOnly date)
    {
        return ActivitySource.StartActivity(ActivityKind.Internal,
            tags: new Dictionary<string, object?>()
            {
                ["city"] = city,
                ["date"] = date
            })!;
    }
    
    public Activity FeelsLikeServiceCalled(string city, DateOnly date, int temperature)
    {
        return ActivitySource.StartActivity(ActivityKind.Internal,
            tags: new Dictionary<string, object?>()
            {
                ["city"] = city,
                ["date"] = date,
                ["temperature"] = temperature
            })!;
    }
    
    public Activity WeatherServiceCalled(string city, int days)
    {
        return ActivitySource.StartActivity(ActivityKind.Internal,
            tags: new Dictionary<string, object?>()
            {
                ["city"] = city,
                ["days"] = days
            })!;
    }
}