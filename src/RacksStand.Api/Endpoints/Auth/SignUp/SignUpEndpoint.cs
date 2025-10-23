namespace RacksStand.Api.Endpoints.Auth.SignUp;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.SignUp.Models;
using ResultifyCore;

public class SignUpEndpoint : Endpoint<SignUpRequest>
{
    private readonly ILogger<SignUpEndpoint> _logger;

    public SignUpEndpoint(
        ILogger<SignUpEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/signup");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Registers a new user");
    }

    public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Signing up a new account.");
        Outcome response = await req.ExecuteAsync(ct);
        IResult result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
