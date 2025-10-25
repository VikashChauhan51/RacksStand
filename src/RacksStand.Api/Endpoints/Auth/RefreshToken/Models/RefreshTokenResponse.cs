namespace RacksStand.Api.Endpoints.Auth.RefreshToken.Models;

public record RefreshTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken);
