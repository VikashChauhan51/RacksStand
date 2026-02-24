using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.ForgotPassword.Models;

public record ForgotPasswordRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("redirect_url")] string RedirectUrl) : ICommand<Outcome>;
