using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.SignIn.Models;

public record SignInRequest(
    [property: JsonPropertyName("email")]string Email,
    [property: JsonPropertyName("password")] string Password) :ICommand<Outcome<SignInResponse?>>;
