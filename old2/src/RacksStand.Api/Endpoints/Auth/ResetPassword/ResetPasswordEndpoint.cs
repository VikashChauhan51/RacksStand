namespace RacksStand.Api.Endpoints.Auth.ResetPassword;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.ResetPassword.Models;
using ResultifyCore;

public class ResetPasswordEndpoint : Endpoint<ResetPasswordRequest>
{
    private readonly ILogger<ResetPasswordEndpoint> _logger;

    public ResetPasswordEndpoint(ILogger<ResetPasswordEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/reset-password");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Reset password using token from email.");
    }

    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Resetting password for user {UserId}.", req.UserId);
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
