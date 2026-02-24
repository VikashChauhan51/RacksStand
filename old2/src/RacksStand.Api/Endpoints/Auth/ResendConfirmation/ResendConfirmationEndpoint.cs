namespace RacksStand.Api.Endpoints.Auth.ResendConfirmation;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.ResendConfirmation.Models;
using ResultifyCore;

public class ResendConfirmationEndpoint : Endpoint<ResendConfirmationRequest>
{
    private readonly ILogger<ResendConfirmationEndpoint> _logger;
    public ResendConfirmationEndpoint(ILogger<ResendConfirmationEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/resend-confirmation");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Resend email confirmation link.");
    }

    public override async Task HandleAsync(ResendConfirmationRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Resend confirmation requested for {Email}", req.Email);
        var resp = await req.ExecuteAsync(ct);
        await Send.ResultAsync(resp.ToResult());
    }
}
