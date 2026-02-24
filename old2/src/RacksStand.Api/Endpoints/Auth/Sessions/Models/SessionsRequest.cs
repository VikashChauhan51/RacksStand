using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Sessions.Models;

public record SessionsRequest() : ICommand<Outcome<IEnumerable<SessionDto>>>;
