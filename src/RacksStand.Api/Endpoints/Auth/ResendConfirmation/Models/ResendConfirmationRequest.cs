using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.ResendConfirmation.Models;

public record ResendConfirmationRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("redirect_url")] string RedirectUrl) : ICommand<Outcome>;
