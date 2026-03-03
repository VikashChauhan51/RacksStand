using OpenTelemetry;
using OpenTelemetry.Logs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RacksStands.Framework.Monitoring.Logging.Enrichers;

public class LogAttributeEnrichmentProcessor : BaseProcessor<LogRecord>
{
    private const string DefaultPrifix = "attribute";
    private const string DefaultAttributeToModify = "{OriginalFormat}";

    private readonly string CustomPrifix;
    public LogAttributeEnrichmentProcessor(string customPrifix)
    {
        CustomPrifix = customPrifix ?? DefaultPrifix;
    }

    public override void OnEnd(LogRecord data)
    {
        if (data == null)
        {
            return;
        }

        if (data.Attributes == null || data.Attributes.Count == 0)
        {
            return;
        }
        string? orginalFormat = data.Attributes
            .FirstOrDefault(x => x.Key == DefaultAttributeToModify).Value?.ToString();

        var modifiedAttributes = new List<KeyValuePair<string, object?>>();
        foreach (var attribute in data.Attributes)
        {
            if (!string.IsNullOrWhiteSpace(attribute.Key) &&
                attribute.Key != DefaultAttributeToModify &&
                !int.TryParse(attribute.Key, out _) &&
                !string.IsNullOrEmpty(orginalFormat) &&
                IsPropertyInTemplate(attribute.Key, orginalFormat))
            {
                modifiedAttributes.Add(new KeyValuePair<string, object?>($"{CustomPrifix}.{attribute.Key}", attribute.Value));
            }
            else
            {
                modifiedAttributes.Add(attribute);
            }
        }
        data.Attributes = modifiedAttributes;
        base.OnEnd(data);
    }

    private static bool IsPropertyInTemplate(string propertyName, string template)
    {
        if (string.IsNullOrEmpty(template))
        {
            return false;
        }

        return template.Contains($"{{{propertyName}}}", StringComparison.Ordinal);
    }
}
