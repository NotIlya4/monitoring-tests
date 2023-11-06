using Microsoft.Extensions.Internal;

namespace Tests;

public class MockSystemClock : ISystemClock
{
    public MockSystemClock(DateTimeOffset time)
    {
        UtcNow = time;
    }

    public DateTimeOffset UtcNow { get; }
}