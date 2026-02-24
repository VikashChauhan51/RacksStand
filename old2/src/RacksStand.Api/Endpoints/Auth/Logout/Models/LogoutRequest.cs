using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Logout.Models;

public record LogoutRequest() : ICommand<Outcome>;
