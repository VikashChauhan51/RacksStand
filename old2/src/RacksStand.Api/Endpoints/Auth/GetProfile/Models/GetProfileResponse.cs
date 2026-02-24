namespace RacksStand.Api.Endpoints.Auth.GetProfile.Models;

public record GetProfileResponse(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName,
    [property: JsonPropertyName("email_confirmed")] bool EmailConfirmed,
    [property: JsonPropertyName("roles")] IEnumerable<string> Roles);
