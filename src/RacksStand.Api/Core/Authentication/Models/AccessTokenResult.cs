namespace RacksStand.Api.Core.Authentication.Models;

public record AccessTokenResult(string Token, string Jti, DateTimeOffset ExpiresAt);
