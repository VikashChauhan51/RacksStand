namespace RacksStand.Api.Endpoints.SignUp;

using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using ResultifyCore;
using RacksStand.Api.Core.Extensions;

public class SignUpEndpoint : Endpoint<SignUpRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<SignUpEndpoint> _logger;
    private readonly IValidator<SignUpRequest> _validator;

    public SignUpEndpoint(
        ILogger<SignUpEndpoint> logger,
        UserManager<ApplicationUser> userManager,
        IValidator<SignUpRequest> validator)
    {
        _userManager = userManager;
        _logger = logger;
        _validator = validator;
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
