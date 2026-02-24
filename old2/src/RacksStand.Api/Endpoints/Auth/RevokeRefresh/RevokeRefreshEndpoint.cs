namespace RacksStand.Api.Endpoints.Auth.RevokeRefresh;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.RevokeRefresh.Models;
using ResultifyCore;

public class RevokeRefreshEndpoint : Endpoint<RevokeRefreshRequest>
{
    private readonly ILogger<RevokeRefreshEndpoint> _logger;
    public RevokeRefreshEndpoint(ILogger<RevokeRefreshEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/revoke-refresh");
        AuthSchemes("Bearer");
        Version(1);
        Summary(s => s.Summary = "Revoke a refresh token by value (owner only).");
    }

    public override async Task HandleAsync(RevokeRefreshRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Revoking refresh token for current user.");
        var outcome = await req.ExecuteAsync(ct);
        await Send.ResultAsync(outcome.ToResult());
    }
}
