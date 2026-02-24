using System.Diagnostics;

namespace RacksStands.Framework.Infrastructure.Tracing;

public static class TracingHelper
{
    public static Activity? StartActivity(string name, ActivityKind kind = ActivityKind.Internal)
    {
        var activitySource = new ActivitySource("RacksStands.Infrastructure");
        return activitySource.StartActivity(name, kind);
    }

    public static void AddEnricher(Activity activity, string key, object? value)
    {
        if (activity != null && value != null)
        {
            activity.SetTag(key, value);
        }
    }
}
