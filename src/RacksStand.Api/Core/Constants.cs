namespace RacksStand.Api.Core;

public static class Constants
{
    public const string AuthenticationSectionName = "Authentication";
    public const string CachingSectionName = "Caching";
    public const int MaximumKeyLength = 1024;
    public const int MaxPayloadSizeBytes = 1024 * 1024; // 1 MB
    public const int MaxRetryCount = 3;
    public const int DefaultCacheExpirationMinutes = 5;
    public const int RetryDelayMilliseconds = 200;
    public const string RetryAfterHeader = "Retry-After";
    public const string PollySectionName = "Polly";
    public const string AuthorizationHeader = "Authorization";
    public const string CorsOpenPolicyName = "OpenCorsPolicy";
    public const string CorsAllowAllWithCredentialsName = "CertificateCorsPolicy";
    public const string CorsSectionName = "Cors:AllowedOrigins";
}
