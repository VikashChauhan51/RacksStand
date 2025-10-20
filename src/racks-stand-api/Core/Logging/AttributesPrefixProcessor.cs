namespace RacksStand.Api.Core.Logging;

using OpenTelemetry;
using OpenTelemetry.Logs;
using System.Diagnostics;

public class AttributesPrefixProcessor : BaseProcessor<LogRecord>
{
    private readonly string _prefix;

    public AttributesPrefixProcessor(string prefix = "Attributes.")
    {
        _prefix = prefix;
    }

    public override void OnEnd(LogRecord data)
    {
        if (data.Attributes == null)
            return;

        var newAttributes = new List<KeyValuePair<string, object?>>();

        foreach (var attr in data.Attributes)
        {
            // If it’s already prefixed, skip
            if (attr.Key.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
                newAttributes.Add(attr);
            else
                newAttributes.Add(new KeyValuePair<string, object?>(_prefix + attr.Key, attr.Value));
        }

        // Replace original attributes (note: this is a mutable copy)
        data.Attributes = newAttributes;
    }
}

