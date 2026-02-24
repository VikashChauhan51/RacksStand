using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OpenTelemetry;
using OpenTelemetry.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacksStands.Framework.Infrastructure.Tracing.Enrichers;

public class HttpContextEnrichmentProcessor: BaseProcessor<Activity>
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public HttpContextEnrichmentProcessor(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public override void OnStart(Activity data)
    {
        if (data==null)
        {
            return;
        }

        var httpContext = _httpContextAccessor.HttpContext;
        string env = _webHostEnvironment.EnvironmentName;
        string correlationId = httpContext?.TraceIdentifier ?? TracingHelper.GenerateTraceIdentifier();
        string? userId = httpContext?.User?.Identity?.IsAuthenticated == true ? httpContext.User.Identity.Name : "anonymous";

        SetTagIfNotNull(data,ActivityTagConstants.Environment, env);
        SetTagIfNotNull(data,ActivityTagConstants.CorelationId, correlationId);
        SetTagIfNotNull(data, ActivityTagConstants.UserId, userId);
        base.OnStart(data);
    }

    private static void SetTagIfNotNull(Activity activity, string tagName, object? value)
    {
        if (value != null)
        {
            activity.SetTag(tagName, value);
        }
    }
}
