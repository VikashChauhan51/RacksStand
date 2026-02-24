using FastEndpoints;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.Mfa.Models;

public record AuthenticatorSetupRequest() : ICommand<Outcome<AuthenticatorSetupResponse?>>;
