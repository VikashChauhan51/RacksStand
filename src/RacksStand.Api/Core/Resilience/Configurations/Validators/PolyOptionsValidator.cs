namespace RacksStand.Api.Core.Resilience.Configurations.Validators;

public class PolyOptionsValidator : AbstractValidator<PolyOptions>
{
    public PolyOptionsValidator()
    {
        RuleFor(x => x.RetryCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RetryCount must be 0 or greater.");

        RuleFor(x => x.DelayInMilliseconds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DelayInMilliseconds must be 0 or greater.");

        RuleFor(x => x.CircuitBreakerFailureThreshold)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerFailureThreshold must be greater than 0.");

        RuleFor(x => x.CircuitBreakerDurationInSeconds)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerDurationInSeconds must be greater than 0.");
    }
}
