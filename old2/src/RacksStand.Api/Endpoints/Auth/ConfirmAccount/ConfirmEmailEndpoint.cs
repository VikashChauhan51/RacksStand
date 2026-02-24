using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.ConfirmAccount.Models;

namespace RacksStand.Api.Endpoints.Auth.ConfirmAccount;

public class ConfirmEmailEndpoint : Endpoint<ConfirmEmailRequest>
{
    private readonly ILogger<ConfirmEmailEndpoint> _logger;

    public ConfirmEmailEndpoint(ILogger<ConfirmEmailEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/confirm-email");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Confirms a user's email using a token");
    }

    public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Confirming email for user {UserId}", req.UserId);
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}

