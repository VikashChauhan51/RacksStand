namespace RacksStand.Api.Endpoints.Auth.Sessions;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Sessions.Models;
using ResultifyCore;

public class SessionsEndpoint : Endpoint<SessionsRequest>
{
    private readonly ILogger<SessionsEndpoint> _logger;
    public SessionsEndpoint(ILogger<SessionsEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Get("/api/auth/sessions");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "List active sessions (refresh tokens) for current user.");
    }

    public override async Task HandleAsync(SessionsRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Listing sessions for current user.");
        var response = await req.ExecuteAsync(ct);
        await Send.ResultAsync(response.ToResult());
    }
}
