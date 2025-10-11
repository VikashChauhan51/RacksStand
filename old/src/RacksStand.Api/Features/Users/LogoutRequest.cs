namespace RacksStand.Api.Features.Users;

public record LogoutRequest
(
    string AccessToken,
    string RefreshToken
);
