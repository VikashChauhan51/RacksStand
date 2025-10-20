namespace RacksStand.Api.Core.Middlewares;

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class LoggingEnrichmentMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<LoggingEnrichmentMiddleware> _logger;

    public LoggingEnrichmentMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILogger<LoggingEnrichmentMiddleware> logger)
    {
        _next = next;
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var activity = Activity.Current;

        string environment = _environment.EnvironmentName;
        string application = _environment.ApplicationName;
        string threadId = Thread.CurrentThread.ManagedThreadId.ToString();
        string? correlationId = context.TraceIdentifier;
        string? clientIp = context.Connection.RemoteIpAddress?.ToString();
        string? requestPath = context.Request.Path;
        string? requestHost = context.Request.Host.Value;
        string? userAgent = context.Request.Headers["User-Agent"].ToString();
        string? contentType = context.Request.ContentType;
        string? httpMethod = context.Request.Method;
        string? scheme = context.Request.Scheme;
        string? traceId = activity?.TraceId.ToString();
        string? spanId = activity?.SpanId.ToString();

        var process = Process.GetCurrentProcess();
        var osDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        var osArchitecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();
        var runtimeVersion = Environment.Version.ToString();
        var osPlatform = Environment.OSVersion.Platform.ToString();

        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["AppName"] = application,
            ["Environment"] = environment,
            ["ThreadId"] = threadId,
            ["CorrelationId"] = correlationId,
            ["ClientIp"] = clientIp,
            ["RequestPath"] = requestPath,
            ["RequestHost"] = requestHost,
            ["UserAgent"] = userAgent,
            ["ContentType"] = contentType,
            ["HttpMethod"] = httpMethod,
            ["Scheme"] = scheme,
            ["TraceId"] = traceId,
            ["SpanId"] = spanId,
            ["ProcessId"] = process.Id,
            ["ProcessName"] = process.ProcessName,
            ["ThreadCount"] = process.Threads.Count,
            ["WorkingSet"] = process.WorkingSet64,
            ["OsDescription"] = osDescription,
            ["OsArchitecture"] = osArchitecture,
            ["OsPlatform"] = osPlatform,
            ["RuntimeVersion"] = runtimeVersion
        }))
        {
            await _next(context);
        }
    }
}

