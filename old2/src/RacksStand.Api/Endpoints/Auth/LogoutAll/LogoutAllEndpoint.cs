namespace RacksStand.Api.Endpoints.Auth.LogoutAll;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.LogoutAll.Models;
using ResultifyCore;

public class LogoutAllEndpoint : Endpoint<LogoutAllRequest>
{
    private readonly ILogger<LogoutAllEndpoint> _logger;
    public LogoutAllEndpoint(ILogger<LogoutAllEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/logout-all");
        AuthSchemes("Bearer");
        Version(1);
        Summary(s => s.Summary = "Logout from all sessions (revoke all access and refresh tokens for current user).");
    }

    public override async Task HandleAsync(LogoutAllRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Logging out all sessions for current user.");
        var response = await req.ExecuteAsync(ct);
        await Send.ResultAsync(response.ToResult());
    }
}
