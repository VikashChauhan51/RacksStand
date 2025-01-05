namespace RacksStand.Api.Features.SignOut;

public record LogoutRequest
(
    string AccessToken,
    string RefreshToken
);
