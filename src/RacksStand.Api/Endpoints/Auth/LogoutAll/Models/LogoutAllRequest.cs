using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.LogoutAll.Models;

public record LogoutAllRequest() : ICommand<Outcome>;
