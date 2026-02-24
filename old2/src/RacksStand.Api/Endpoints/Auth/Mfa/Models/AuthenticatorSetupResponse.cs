namespace RacksStand.Api.Endpoints.Auth.Mfa.Models;

public record AuthenticatorSetupResponse(
    [property: JsonPropertyName("key")] string Key,
    [property: JsonPropertyName("manual_entry_key")] string ManualEntryKey);
