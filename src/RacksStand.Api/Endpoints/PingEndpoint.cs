namespace RacksStand.Api.Endpoints;

using FastEndpoints;

public class PingEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/api/ping"); 
        AllowAnonymous();
        Summary(s => s.Summary = "Ping / health check endpoint");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(ct);
    }
}
