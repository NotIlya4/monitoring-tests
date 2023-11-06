using Service.Monitoring;

namespace Service.Services;

public interface IFeelsLikeService
{
    string HowItFeels(string city, DateOnly date, int temperature);
}

public class FeelsLikeService : IFeelsLikeService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public string HowItFeels(string city, DateOnly date, int temperature)
    {
        return Summaries[Random.Shared.Next(Summaries.Length)];
    }
}

public class FeelsLikeMonitoringService : IFeelsLikeService
{
    private readonly IFeelsLikeService _service;
    private readonly AppActivitySource _activitySource;

    public FeelsLikeMonitoringService(IFeelsLikeService service, AppActivitySource activitySource)
    {
        _service = service;
        _activitySource = activitySource;
    }
    
    public string HowItFeels(string city, DateOnly date, int temperature)
    {
        using var activity = _activitySource.FeelsLikeServiceCalled(city, date, temperature);

        var howItFeels = _service.HowItFeels(city, date, temperature);

        activity.SetTag("howItFeels", howItFeels);
        return howItFeels;
    }
}