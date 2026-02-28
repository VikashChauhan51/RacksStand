using System.Collections.Generic;
using System.Diagnostics;

namespace RacksStands.Framework.Monitoring.Tracking;

public static class TracingHelper
{
    private static readonly ActivitySource ActivitySource = new("RacksStands.App");
    public static Activity? StartActivity(string name, ActivityKind kind = ActivityKind.Internal)
    {
        return ActivitySource.StartActivity(name, kind);
    }

    public static Activity? StartActivity(string name, ActivityKind kind = ActivityKind.Internal, IEnumerable<KeyValuePair<string, object>> tags = null)
    {
        var activity = TracingHelper.StartActivity(name, kind);
        if (tags != null && activity != null)
        {
            foreach (var tag in tags)
            {
                activity.SetTag(tag.Key, tag.Value);
            }
        }
        return activity;
    }

    public static Activity? StartActivity(string name, ActivityContext parentContext)
    {
        return ActivitySource.StartActivity(name, ActivityKind.Internal, parentContext);
    }

    public static string GenerateTraceIdentifier()
    {
        var traceId = Activity.Current?.TraceId.ToString();
        if (!string.IsNullOrWhiteSpace(traceId))
        {
            return traceId;
        }
        traceId = ActivityTraceId.CreateRandom().ToString();
        var spanId = ActivitySpanId.CreateRandom().ToString();
        return $"00-{traceId}-{spanId}-01";
    }
    public static string GenerateTraceState()
    {
        if (!string.IsNullOrEmpty(Activity.Current?.TraceStateString))
        {
            return Activity.Current.TraceStateString;
        }
        return $"service=RacksStands.App";
    }
}
