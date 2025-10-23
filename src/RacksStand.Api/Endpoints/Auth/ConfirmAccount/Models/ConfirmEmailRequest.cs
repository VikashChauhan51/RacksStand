using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.ConfirmAccount.Models;

public record ConfirmEmailRequest(
    [property: JsonPropertyName("user_id")] string UserId,
    [property: JsonPropertyName("code")] string Code) : ICommand<Outcome>;
