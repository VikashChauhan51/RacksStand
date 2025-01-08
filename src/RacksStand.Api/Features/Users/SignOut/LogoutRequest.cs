namespace RacksStand.Api.Features.Users.SignOut;

public record LogoutRequest
(
    string AccessToken,
    string RefreshToken
);
