namespace RacksStand.Api.Endpoints.Auth.SignIn;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.SignIn.Models;
using ResultifyCore;

public class SignInEndpoint : Endpoint<SignInRequest>
{
    private readonly ILogger<SignInEndpoint> _logger;

    public SignInEndpoint(
        ILogger<SignInEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/auth/signin");
        AllowAnonymous();
        Version(1);
        Summary(s => s.Summary = "Login user");
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Signing up a new account.");
        Outcome<SignInResponse> response = await req.ExecuteAsync(ct);
        IResult result = response.ToResult();
        await Send.ResultAsync(result);
    }
}
