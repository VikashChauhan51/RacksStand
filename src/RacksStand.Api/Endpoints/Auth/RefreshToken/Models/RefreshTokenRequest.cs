using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.RefreshToken.Models;

public record RefreshTokenRequest([property: JsonPropertyName("refresh_token")] string RefreshToken) :ICommand<Outcome<RefreshTokenResponse?>>;
