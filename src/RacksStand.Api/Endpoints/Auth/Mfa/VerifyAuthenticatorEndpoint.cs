namespace RacksStand.Api.Endpoints.Auth.Mfa;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public class VerifyAuthenticatorEndpoint : Endpoint<VerifyAuthenticatorRequest>
{
    private readonly ILogger<VerifyAuthenticatorEndpoint> _logger;
    public VerifyAuthenticatorEndpoint(ILogger<VerifyAuthenticatorEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/mfa/verify-authenticator");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Verify authenticator code and enable 2FA for user.");
    }

    public override async Task HandleAsync(VerifyAuthenticatorRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Verifying authenticator code for current user.");
        var resp = await req.ExecuteAsync(ct);
        await Send.ResultAsync(resp.ToResult());
    }
}
