namespace RacksStand.Api.Endpoints.Auth.Sessions.Models;

public record SessionDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("created_at")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("expires_at")] DateTimeOffset ExpiresAt,
    [property: JsonPropertyName("deleted_at")] DateTimeOffset? DeletedAt);
