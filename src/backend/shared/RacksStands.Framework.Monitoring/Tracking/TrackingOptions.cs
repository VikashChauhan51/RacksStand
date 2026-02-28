using FluentValidation;

namespace RacksStands.Framework.Monitoring.Tracking;

public class TrackingOptions
{
    public bool EnableTracing { get; set; }
    public string? ServiceName { get; set; }
}

public class TrackingOptionsValidator : AbstractValidator<TrackingOptions>
{
    public TrackingOptionsValidator()
    {
        RuleFor(x => x.ServiceName).NotEmpty().When(x => x.EnableTracing);
    }
}
