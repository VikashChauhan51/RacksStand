using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.RefreshToken.Models;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.RefreshToken;

public class RefreshTokenEndpoint : Endpoint<RefreshTokenRequest>
{
    private readonly ILogger<RefreshTokenEndpoint> _logger;

    public RefreshTokenEndpoint(
        ILogger<RefreshTokenEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/refresh");
        AllowAnonymous();
        Version(1);
        Summary(s =>
        {
            s.Summary = "Generates a new access token using a valid refresh token.";
            s.Description = "Exchanges a valid refresh token for a new access and refresh token pair.";
        });
    }

    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Generating new access token with refresh token.");
        Outcome<RefreshTokenResponse?> response = await req.ExecuteAsync(ct);
        IResult result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
