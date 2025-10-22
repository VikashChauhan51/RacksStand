using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.SignUp;

public record SignUpRequest(
    [property: JsonPropertyName("full_name")] string FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("agreed_terms_confirmed")] bool AgreedTermsConfirmed) : ICommand<Outcome>;
