namespace RacksStand.Api.Core.Logging;

using OpenTelemetry;
using OpenTelemetry.Logs;
using System.Collections.Generic;

public class AttributesPrefixProcessor : BaseProcessor<LogRecord>
{
    private const string DefaultPrefix = "Attribute";
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

    public AttributesPrefixProcessor(string prefix = DefaultPrefix)
    {
        _prefix = !string.IsNullOrWhiteSpace(prefix) ? prefix : DefaultPrefix;
    }

    public override void OnEnd(LogRecord data)
    {
        if (data.Attributes == null || data.Attributes.Count == 0)
            return;

        var newAttributes = new List<KeyValuePair<string, object?>>();

        foreach (var attr in data.Attributes)
        {
            if (_excludedKeys.Contains(attr.Key))
            {
                newAttributes.Add(attr);
                continue;
            }

            if (IsStructuredProperty(attr.Key))
            {
                newAttributes.Add(new KeyValuePair<string, object?>($"{_prefix}.{attr.Key}", attr.Value));
            }
            else
            {
                newAttributes.Add(attr);
            }
        }

        data.Attributes = newAttributes;
    }

    private static bool IsStructuredProperty(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;

        if (key.Equals("OriginalFormat", StringComparison.OrdinalIgnoreCase) ||
            key.Equals("State", StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    }
}
