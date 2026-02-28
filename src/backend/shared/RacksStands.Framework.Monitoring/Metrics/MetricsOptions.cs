using FluentValidation;

namespace RacksStands.Framework.Monitoring.Metrics;

public class MetricsOptions
{
    public bool EnableMetrics { get; set; }
    public string? MetricsEndpoint { get; set; }
}

public class MetricsOptionsValidator : AbstractValidator<MetricsOptions>
{
    public MetricsOptionsValidator()
    {
        RuleFor(x => x.MetricsEndpoint).NotEmpty().When(x => x.EnableMetrics);
    }
}
