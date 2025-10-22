namespace RacksStand.Api.Core.Configurations.Validators;

public class AuthenticationConfigurationValidator : AbstractValidator<AuthenticationConfiguration>
{
    public AuthenticationConfigurationValidator()
    {
        RuleFor(x => x.JwtSecret)
            .NotEmpty().WithMessage("JWT Secret must be configured.")
            .MinimumLength(32).WithMessage("JWT Secret should be at least 32 characters for security.");

        RuleFor(x => x.Issuer).NotEmpty();
        RuleFor(x => x.Audience).NotEmpty();
        RuleFor(x => x.AccessTokenExpirationMinutes).GreaterThan(0);
        RuleFor(x => x.RefreshTokenExpirationDays).GreaterThan(0);
    }
}

