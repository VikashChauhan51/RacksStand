namespace RacksStand.Api.Core.Logging;

using OpenTelemetry;
using OpenTelemetry.Logs;
using System.Collections.Generic;

public class AttributesPrefixProcessor : BaseProcessor<LogRecord>
{
    private readonly string _prefix;
    private static readonly HashSet<string> _excludedKeys = new(StringComparer.OrdinalIgnoreCase)
    {
        "OriginalFormat",
        "{OriginalFormat}",
        "EventId",
        "State",
        "SourceContext",
        "TraceId",
        "SpanId",
        "LogLevel",
        "CategoryName",
        "RenderedMessage",
        "Message"
    };

    public AttributesPrefixProcessor(string prefix = "Attributes")
    {
        _prefix = prefix;
        if (string.IsNullOrWhiteSpace(_prefix))
        {
            _prefix = "Attributes";
        }
    }

    public override void OnEnd(LogRecord data)
    {
        if (data.Attributes == null || data.Attributes.Count == 0)
            return;

        var groupedAttributes = new Dictionary<string, object?>();
        var retainedAttributes = new List<KeyValuePair<string, object?>>();
        foreach (var attr in data.Attributes)
        {
            // Keep telemetry/system-level attributes as-is
            if (_excludedKeys.Contains(attr.Key))
            {
                retainedAttributes.Add(attr);
                continue;
            }

            // Only consider structured log properties (ignore things like "State")
            if (IsStructuredProperty(attr.Key))
            {
                groupedAttributes[$"{_prefix}.{attr.Key}"] = LogSanitizer.SanitizeAndMask(attr.Value?.ToString());
            }
            else
            {
                retainedAttributes.Add(attr);
            }
        }

        var newAttributes = new List<KeyValuePair<string, object?>>(retainedAttributes);
        // add the grouped Attributes collection
        if (groupedAttributes.Count > 0)
        {
            newAttributes.Add(new KeyValuePair<string, object?>(_prefix, groupedAttributes));
        }

        data.Attributes = newAttributes;
    }

    private static bool IsStructuredProperty(string key)
    {
        // Basic heuristic: exclude known logger metadata patterns
        if (string.IsNullOrWhiteSpace(key))
            return false;

        // Exclude known internal/system fields only
        if (key.Equals("OriginalFormat", StringComparison.OrdinalIgnoreCase) ||
            key.Equals("State", StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    }
}
