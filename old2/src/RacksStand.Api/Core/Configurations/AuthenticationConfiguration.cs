namespace RacksStand.Api.Core.Configurations;

public sealed class AuthenticationConfiguration
{
    public string JwtSecret { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public int AccessTokenExpirationMinutes { get; init; } = 60;
    public int RefreshTokenExpirationDays { get; init; } = 30;
    public int MaxRetryAttempts { get; init; } = 5;
    public int LockoutDurationMinutes { get; init; } = 15;

    public int DefaultRetryDelayMilliseconds { get; init; } = 200;
}

