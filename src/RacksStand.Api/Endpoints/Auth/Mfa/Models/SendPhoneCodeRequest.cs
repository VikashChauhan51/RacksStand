using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Mfa.Models;

public record SendPhoneCodeRequest([property: JsonPropertyName("phone_number")] string PhoneNumber) : ICommand<Outcome>;
