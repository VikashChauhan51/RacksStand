namespace RacksStand.Api.Endpoints.Auth.GetProfile;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.GetProfile.Models;
using ResultifyCore;

public class GetProfileEndpoint : Endpoint<GetProfileRequest>
{
    private readonly ILogger<GetProfileEndpoint> _logger;

    public GetProfileEndpoint(ILogger<GetProfileEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/api/auth/profile");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Get profile information for the authenticated user.");
    }

    public override async Task HandleAsync(GetProfileRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Getting profile for current user.");
        var response = await req.ExecuteAsync(ct);
        var result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
