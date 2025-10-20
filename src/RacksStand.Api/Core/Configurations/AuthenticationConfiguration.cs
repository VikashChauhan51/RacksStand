namespace RacksStand.Api.Core.Configurations;

public sealed class AuthenticationConfiguration
{
    public string SecretForKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}

