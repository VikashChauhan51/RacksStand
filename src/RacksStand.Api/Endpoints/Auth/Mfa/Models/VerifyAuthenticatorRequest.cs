using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Mfa.Models;

public record VerifyAuthenticatorRequest([property: JsonPropertyName("code")] string Code) : ICommand<Outcome>;
