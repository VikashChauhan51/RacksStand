namespace RacksStand.Api.Core.Configurations;

public sealed class AuthenticationConfiguration
{
    public string JwtSecret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int AccessTokenExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 30;
}

