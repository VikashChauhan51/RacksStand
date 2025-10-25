namespace RacksStand.Api.Endpoints.Auth.SignIn.Models;

public record SignInResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken);
