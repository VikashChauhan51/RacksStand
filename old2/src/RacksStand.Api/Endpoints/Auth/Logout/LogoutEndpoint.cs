namespace RacksStand.Api.Endpoints.Auth.Logout;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Logout.Models;
using ResultifyCore;

public class LogoutEndpoint : Endpoint<LogoutRequest>
{
    private readonly ILogger<LogoutEndpoint> _logger;

    public LogoutEndpoint(ILogger<LogoutEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/logout");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Logs out the current user and revokes the current access token.");
    }

    public override async Task HandleAsync(LogoutRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Logging out current user.");
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
