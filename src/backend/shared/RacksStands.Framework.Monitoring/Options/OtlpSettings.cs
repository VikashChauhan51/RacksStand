using System;
using System.ComponentModel.DataAnnotations;

namespace RacksStands.Framework.Monitoring.Options;

public sealed class OtlpSettings
{
    [Required]
    public Uri Endpoint { get; set; } = new Uri("http://localhost:4317");

    public string? Headers { get; set; }
}
