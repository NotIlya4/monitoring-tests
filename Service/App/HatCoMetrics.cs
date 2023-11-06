using System.Diagnostics.Metrics;

namespace Service;

public class HatCoMetrics
{
    public Counter<int> HatsSoldCounter { get; }

    public HatCoMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("HatCo.Store");
        HatsSoldCounter = meter.CreateCounter<int>("hatco.store.hats_sold", "The count of sold hats");
    }

    public void HatsSold(int quantity)
    {
        HatsSoldCounter.Add(quantity);
    }
}