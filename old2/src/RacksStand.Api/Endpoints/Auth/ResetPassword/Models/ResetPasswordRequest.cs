using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.ResetPassword.Models;

public record ResetPasswordRequest(
    [property: JsonPropertyName("user_id")] string UserId,
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("new_password")] string NewPassword) : ICommand<Outcome>;
