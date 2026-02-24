namespace RacksStand.Api.Endpoints.Auth.Mfa;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public class VerifyPhoneCodeEndpoint : Endpoint<VerifyPhoneCodeRequest>
{
    private readonly ILogger<VerifyPhoneCodeEndpoint> _logger;
    public VerifyPhoneCodeEndpoint(ILogger<VerifyPhoneCodeEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/mfa/verify-sms");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Verify phone code and associate phone number with user.");
    }

    public override async Task HandleAsync(VerifyPhoneCodeRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Verifying phone code for {Phone}", req.PhoneNumber);
        var resp = await req.ExecuteAsync(ct);
        await Send.ResultAsync(resp.ToResult());
    }
}
