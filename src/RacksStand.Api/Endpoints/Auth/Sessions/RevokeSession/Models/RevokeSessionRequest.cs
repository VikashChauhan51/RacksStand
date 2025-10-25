using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Sessions.RevokeSession.Models;

public record RevokeSessionRequest([property: JsonPropertyName("session_id")] string SessionId) : ICommand<Outcome>;
