using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.SignUp.Models;

public record SignUpRequest(
    [property: JsonPropertyName("full_name")] string FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("agreed_terms_confirmed")] bool AgreedTermsConfirmed,
    [property: JsonPropertyName("redirect_url")] string RedirectUrl) : ICommand<Outcome>;
