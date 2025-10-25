using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.RevokeRefresh.Models;

public record RevokeRefreshRequest([property: JsonPropertyName("refresh_token")] string RefreshToken) : ICommand<Outcome>;
