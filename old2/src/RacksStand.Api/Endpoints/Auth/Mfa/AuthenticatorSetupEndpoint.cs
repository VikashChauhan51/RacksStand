namespace RacksStand.Api.Endpoints.Auth.Mfa;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public class AuthenticatorSetupEndpoint : Endpoint<AuthenticatorSetupRequest>
{
    private readonly ILogger<AuthenticatorSetupEndpoint> _logger;
    public AuthenticatorSetupEndpoint(ILogger<AuthenticatorSetupEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/mfa/authenticator-setup");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Get or generate authenticator app key for the authenticated user.");
    }

    public override async Task HandleAsync(AuthenticatorSetupRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Generating/returning authenticator key for current user.");
        var resp = await req.ExecuteAsync(ct);
        await Send.ResultAsync(resp.ToResult());
    }
}
