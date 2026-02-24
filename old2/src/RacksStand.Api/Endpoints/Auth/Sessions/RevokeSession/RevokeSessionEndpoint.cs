namespace RacksStand.Api.Endpoints.Auth.Sessions.RevokeSession;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Sessions.RevokeSession.Models;
using ResultifyCore;

public class RevokeSessionEndpoint : Endpoint<RevokeSessionRequest>
{
    private readonly ILogger<RevokeSessionEndpoint> _logger;
    public RevokeSessionEndpoint(ILogger<RevokeSessionEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/sessions/revoke");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Revoke a specific session (refresh token) by ID.");
    }

    public override async Task HandleAsync(RevokeSessionRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Revoking session {SessionId}", req.SessionId);
        var response = await req.ExecuteAsync(ct);
        await Send.ResultAsync(response.ToResult());
    }
}
