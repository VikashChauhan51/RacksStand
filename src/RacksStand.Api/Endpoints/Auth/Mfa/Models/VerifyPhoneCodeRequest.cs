using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Mfa.Models;

public record VerifyPhoneCodeRequest(
    [property: JsonPropertyName("phone_number")] string PhoneNumber,
    [property: JsonPropertyName("code")] string Code) : ICommand<Outcome>;
