namespace RacksStand.Api.Core.Authentication.Models;

public record RefreshTokenResult(string PlainToken, string HashedToken, DateTimeOffset ExpiresAt);
