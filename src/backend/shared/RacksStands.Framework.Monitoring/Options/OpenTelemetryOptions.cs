using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RacksStands.Framework.Monitoring.Options;

public sealed class OpenTelemetryOptions : IValidatableObject
{
    public const string SectionName = "OpenTelemetry";

    public bool Enable { get; set; } = true;

    public OtlpSettings? Otlp { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Otlp != null && Otlp.Endpoint == null)
        {
            yield return new ValidationResult("OTLP endpoint is required when OTLP settings are provided.", new[] { nameof(Otlp) });

        }
    }
}
