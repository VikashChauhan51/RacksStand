namespace RacksStand.Api.Endpoints.Auth.ForgotPassword;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.ForgotPassword.Models;
using ResultifyCore;

public class ForgotPasswordEndpoint : Endpoint<ForgotPasswordRequest>
{
    private readonly ILogger<ForgotPasswordEndpoint> _logger;

    public ForgotPasswordEndpoint(ILogger<ForgotPasswordEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/forgot-password");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Request a password reset email.");
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Processing forgot password request for {Email}.", req.Email);
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
