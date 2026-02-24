using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.ChangePassword.Models;

public record ChangePasswordRequest(
    [property: JsonPropertyName("current_password")] string CurrentPassword,
    [property: JsonPropertyName("new_password")] string NewPassword) : ICommand<Outcome>;
