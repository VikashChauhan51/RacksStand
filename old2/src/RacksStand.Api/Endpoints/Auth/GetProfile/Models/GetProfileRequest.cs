using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.GetProfile.Models;

public record GetProfileRequest() : ICommand<Outcome<GetProfileResponse?>>;
