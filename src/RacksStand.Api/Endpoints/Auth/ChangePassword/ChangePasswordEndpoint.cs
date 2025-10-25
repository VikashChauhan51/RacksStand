namespace RacksStand.Api.Endpoints.Auth.ChangePassword;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.ChangePassword.Models;
using ResultifyCore;

public class ChangePasswordEndpoint : Endpoint<ChangePasswordRequest>
{
    private readonly ILogger<ChangePasswordEndpoint> _logger;

    public ChangePasswordEndpoint(ILogger<ChangePasswordEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/change-password");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Change password for authenticated user.");
    }

    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Changing password for current user.");
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
