using FastEndpoints;

namespace RacksStand.Api.Endpoints.Auth.SignUp.Models;

public record SignUpEmailRequest(string UserId, string RedirectUrl): ICommand;
