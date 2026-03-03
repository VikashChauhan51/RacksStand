using OpenTelemetry;
using System;
using System.Diagnostics;
using System.Linq;

namespace RacksStands.Framework.Monitoring.Tracking.Enrichers;

public sealed class SecureLogAttributeEnrichmentProcessor : BaseProcessor<Activity>
{
    // List of sensitive tag keys to redact
    private static readonly string[] SensitiveKeys = new[] { "password", "ssn", "creditcard", "email", "phone" };
    private const string Redacted = "[REDACTED]";

    public override void OnStart(Activity data)
    {
        if (data == null)
        {
            return;
        }
        foreach (var tag in data.Tags)
        {
            if (tag.Key.StartsWith("otel.", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            // Redact sensitive fields
            var value = SensitiveKeys.Any(s => tag.Key.Contains(s, StringComparison.OrdinalIgnoreCase)) ? Redacted : tag.Value;
            // Add more filters or transformations here if needed
            data.SetTag($"log.{tag.Key}", value);
        }
        base.OnStart(data);
    }
}
